module Automation
  class Paket < Thor
    include ::Automation::Utils

    namespace :paket

    desc 'get_latest_version', 'upgrades to the latest version of paket'
    def get_latest_version
      paket_bootstrapper
    end

    desc 'clear', 'clears out all the paket packages'
    def clear
      FileUtils.rm_rf packages_folder
    end

    desc 'pack', 'package the lib into a nuget library'
    def pack
      invoke 'compile:project', ['compile_units/all.compile']
      %w/response_files
        specs
      /.each do |folder|
        FileUtils.rm_rf File.join(settings.artifacts_dir, folder)
      end
      paket("pack output #{settings.distribution.paket.dir}")
    end

    desc 'push', 'push the nuget package upto nuget'
    def push
      invoke :pack
      package = File.join(settings.distribution.paket.dir, "developwithpassion.specifications.#{settings.version}.nupkg")
      paket("push url #{settings.distribution.nuget.url} file #{package} apikey #{settings.distribution.nuget.key}")
    end

    [
      'install',
      'update',
    ].each do |command|
      self.instance_eval do
        desc command, "Run the packet #{command} command"
        define_method command do
          paket command
        end
      end
    end

    no_commands do
      def paket(command)
        base_command = "automation/tools/paket/paket"
        full_command = "#{base_command} #{command}"
        system full_command
      end

      def paket_bootstrapper
        command = "automation/tools/paket/paket.bootstrapper"
        system command
      end

      def packages_folder
        settings.paket.packages_folder
      end
    end
  end
end

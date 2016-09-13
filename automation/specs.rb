module Automation
  class Specs < Thor
    include ::Automation::Compile::CompileUnitResolution
    namespace :specs

    desc 'view', 'view the spec report'
    def view 
      system "start #{settings.specs.report_dir}/#{settings.project}.specs.html"
    end

    method_option :flags, type: :hash, default: { }
    desc 'run_them','run the specs for the project'
    def run_them(*compile_files)
      compile_files.each do |file|
        compile = ::Automation::Compile::Compile.new
        unit = get_compile_unit(file)
        compile.project(file)
      end

      (settings.libs + settings.config_files).each do|file|
        FileUtils.cp(file, settings.artifacts_dir)
      end

      flags = default_options.merge(options[:flags])

      line = build_runner_line(settings.specs.flags.merge(flags), settings.specs.assemblies)
      system(line)
    end

    no_commands do
      def default_options
        { 
          "no-teamcity-autodetect" => nil,
          "no-appveyor-autodetect" => nil,
          "silent" => nil,
        }
      end

      def build_runner_line(flags, assemblies)
        exe = settings.specs.exe

        parameters = []
        flags.each do |key, value|
          parameters << "--#{key}#{value.nil? ? "" : "=#{value}"}" 
        end
        parameters = parameters + assemblies
        command_line = "./#{exe} #{parameters.join(' ')}"

        IO.write("mspec_command.output", command_line)
        command_line
      end
    end
  end
end

module Automation
  class General < Thor
    namespace :automation

    desc 'init', 'kick off task'
    def init
      settings.automation.folders_to_create.each do |folder|
        FileUtils.mkdir_p folder unless Dir.exists?(folder)
      end
    end

    desc 'clean', 'cleans out old files'
    def clean
      settings.automation.folders_to_clean.each do |folder|
        FileUtils.rm_rf folder 
      end
    end

    desc 'expand', 'expands template files'
    def expand(file=nil)
      unless file
        Expansions::CLIInterface.run("ExpansionFile")
        return
      end
      ::Expansions::Startup.run
      ::Expansions::TemplateVisitor.instance.run_using(file)
    end
  end
end

require_relative 'utils'
require_relative 'input_utils'
require_relative 'compile'
require_relative 'specs'
require_relative 'dynamic_edit'
require_relative 'paket'

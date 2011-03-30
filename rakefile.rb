require 'rake'
require 'rake/clean'
require 'fileutils'
require 'erb'
require 'configatron'

=begin Supplementary Build Files

build\utils - Utilities for the build
build\tasks\configuration.rb - Project specific configuration
build\tasks\machine_specs.rb 
build\tasks\deployment.rb - Packaging tasks

=end 

%w[utils tasks].each do|folder|
  Dir.glob(File.join(File.dirname(__FILE__),"build/#{folder}/*.rb")).each do|item|
    require item
  end
end

[
  configatron.artifacts_dir,
  configatron.specs.dir
].each do |item|
  CLEAN.include(item)
end

task :expand_all_template_files do
  TemplateFiles.expand
end

%w[configure expand_all_template_files].each do|build_task|
  Rake::Task[build_task].invoke
end

@project_files = FileList.new("#{configatron.source_dir}/**/*.csproj")

#configuration files
config_files = FileList.new(File.join(configatron.source_dir,'config','*.erb')).select{|fn| ! fn.include?('app.config')}

#target folders that can be run from VS
solution_file = "solution.sln"

task :default => ["specs:run"]

task :init  => :clean do
  [
    configatron.artifacts_dir,
    configatron.specs.dir,
    configatron.specs.report_dir,
    configatron.distribution.zip.dir,
    configatron.distribution.nuget.dir
  ].each do |folder| 
    FileUtils.mkdir_p folder if ! File.exists?(folder)
  end
end


namespace :build do
  desc 'compiles the project'
  task :compile => :init do
    opts = {
        :version => 'v4\Full',
        :switches => { :verbosity => :minimal, :target => :Build },
        :properties => {
          :Configuration => configatron.target,
          :TrackFileAccess => false
        }
      }

      @project_files.each do |project|
        MSBuildRunner.build opts.merge({ :project => project }), 
        { 
            :OutputPath => configatron.artifacts_dir.to_absolute 
        }
      end
  end

  task :from_ide do
    config_files.each do |file|
        [configatron.artifacts_dir].each do|folder|
          FileUtils.cp(file.name_without_template_extension,
          folder.join(file.base_name_without_extension))
        end
    end
  end
  
  task :rebuild => ["clean","compile"]
end

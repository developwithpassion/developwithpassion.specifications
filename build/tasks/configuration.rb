task :configure do
  project = "developwithpassion.specifications"
  target = ENV['target'] || 'Debug'
  
  configs = 
  {
    :project =>  project,
    :target => target,
    :source_dir => "source",
    :artifacts_dir => "artifacts",
    :version => delayed{ENV['VERSION']},
    :version_simple => delayed{configatron.version.gsub(/"/,'')},
    :config_dir => delayed{configatron.source_dir.join("config")},

    :specs =>
    {
      :runner_options => ["-x","example"],
      :tools_folder => delayed{File.join("packages","Machine.Specifications.0.4.21.0","tools")},
      :dir => delayed{configatron.artifacts_dir.join('specs')},
      :report_dir => delayed{configatron.specs.dir.join('report')},
      :assemblies => dynamic{Dir.glob("#{configatron.artifacts_dir}/*specs.dll")},
    },

    :distribution => 
    {
      :dir => "distribution",
      :zip =>
      {
        :dir => delayed{configatron.distribution.dir.join("zips")},
      },
      :nuget =>
      {
        :dir => delayed{configatron.distribution.dir.join("nuget")},
        :key => dynamic{File.read('nuget_key') if File.readable?('nuget_key')}
      }
    }
  }
  configatron.configure_from_hash configs
  configatron.protect_all!
end


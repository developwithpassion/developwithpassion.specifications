namespace :specs do
  desc 'view the spec report'
  task :view do
    system "start #{configatron.specs.report_dir}/#{configatron.project}.specs.html"
  end

  desc 'run the specs for the project'
  task :run  => [:init,:expand_all_template_files,'build:rebuild'] do
    Dir.glob("#{configatron.specs.tools_folder}/*.*").each do|file|
      FileUtils.cp(file,configatron.artifacts_dir)
    end

    sh "#{configatron.artifacts_dir}/mspec-clr4.exe", "--html", "#{configatron.specs.report_dir}/#{configatron.project}.specs.html", *(configatron.specs.runner_options + configatron.specs.assemblies)
  end
end

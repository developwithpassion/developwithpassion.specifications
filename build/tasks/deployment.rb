namespace :dist do
  task :_init => ['build:rebuild', 'specs:run'] do
    @targets = @project_files.map{|project| project.base_name_without_extension}.select{|project| /\.specs/ !~ project}
  end

  def run_package_activity
    @targets.each do|target|
      yield target,"#{target}.???"
    end
  end

  def get_dependencies_section(project)
    file_name = "#{configatron.source_dir}/#{project}/packages.config"
    contents = File.real_all_text_after_skipping_lines(file_name,1)
    contents.gsub(/packages/,'dependencies').gsub(/package/,'dependency')
  end

  desc "build zip distributables for projects"

  task :zip => [ 'dist:_init' ] do
    rm_rf configatron.distribution.zip.dir
    
    cp 'License.txt', configatron.artifacts_dir
    run_package_activity do|target,pattern|
      zip_file = "distribution/zips/#{target}.zip"
      sz = SevenZip.new \
        :tool => 'build/tools/7zip/7za.exe',
        :zip_name => zip_file
      Dir.chdir(configatron.artifacts_dir) do
        sz.zip :files => pattern
      end
    end
  end

  namespace :nu do
    FileUtils.rm 'project.nuspec'
    desc "package as nuget distributables"
    task :build => 'dist:_init' do
      config = 
      {
      }
      template = TemplateFile.new("project.nuspec.erb")
      run_package_activity do|target,pattern|
        generated_template = "#{configatron.distribution.nuget.dir}/#{target}.nuspec"
        
        configatron.core_files = (target != configatron.project ? Dir.glob("#{configatron.artifacts_dir}/#{configatron.project}.???") : [])
        configatron.package_file_list = Dir.glob("#{configatron.artifacts_dir}/#{pattern}")
        configatron.package_name = target
        configatron.package_dependencies = get_dependencies_section(target) 

        template.expand(generated_template)

        opts = ["pack", generated_template,
          "-BasePath", ".",
          "-OutputDirectory", configatron.distribution.nuget.dir]

        sh "build/tools/nuget/NuGet.exe", *(opts)
      end
    end
    
    desc "publish nu packages to nuget"
    task :publish => :build do
      raise "Cannot publish without NuGet access key" if configatron.distribution.nuget.key.nil?
      FileUtils.rm Dir.glob("#{configatron.distribution.nuget.dir}/*examples*")
      FileUtils.rm Dir.glob("#{configatron.distribution.nuget.dir}/*specifications.#{configatron.version_simple}.nupkg")

      Dir.glob("#{configatron.distribution.nuget.dir}/*.nupkg").each do|package|

        sh "build/tools/nuget/NuGet.exe push -source http://packages.nuget.org/v1/ #{package} #{configatron.distribution.nuget.key}" do |ok, status|
          "Nuget failed with status (#{status.exitstatus})" if status.exitstatus > 0
        end

      end
    end
  end
end

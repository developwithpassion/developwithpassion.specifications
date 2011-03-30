require 'win32/registry'
include Win32

class MSBuildRunner
  class << self
    def compile(attributes)
      version = attributes.fetch(:version, 'v4.0.30319')
      projectFile = attributes.fetch(:project).to_absolute
      userProperties = attributes.fetch(:properties, {})
      userSwitches = attributes.fetch(:switches, {})
      
      frameworkDir = File.join(ENV['WINDIR'].dup, 'Microsoft.NET', 'Framework', version)
      
      Registry::HKEY_LOCAL_MACHINE.open("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\#{version}") do |reg|
        frameworkDir = reg['InstallPath', Win32::Registry::REG_SZ]
      end
      
      msbuild = File.join(frameworkDir, 'msbuild.exe')
      
      properties = {
        :BuildInParallel => false,
        :BuildRunner => 'Rake',
        :Configuration => 'Debug'
      }
      properties.merge!(userProperties)
      
      properties = properties.collect { |key, value|
        "/property:#{key}=#{value}"
      }.join " "
      
      switches = {
        :maxcpucount => true,
        :target => 'Build'
      }

      switches.merge!(userSwitches)
      
      switches = switches.collect { |key, value|
        "/#{key}#{":#{value}" unless value.kind_of? TrueClass or value.kind_of? FalseClass}" if value
      }.join " "
      
      sh "#{msbuild.escape} #{projectFile.escape} #{switches} #{properties}"
    end

    def build (msbuild_options, config)
      project = msbuild_options[:project]

      xml = File.read project

      config.inject(xml) do|text,(element,value)|
        xml = text.gsub /<#{element}>.*?<\/#{element}>/, "<#{element}>#{value}</#{element}>"
      end

      project += config.hash.to_s
      File.open(project, "w") { |file| file.puts xml }

      MSBuildRunner.compile msbuild_options.merge({ :project => project })
    end
  end
end

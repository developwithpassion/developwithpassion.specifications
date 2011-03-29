require 'tempfile'

class SevenZip
	attr_accessor :tool, :args, :zip_name
	
	def initialize(params = {})
		@tool = params.fetch(:tool).to_absolute
		@args = params.fetch(:args, 'a')
		@zip_name = params.fetch(:zip_name).to_absolute
	end
	
	def zip(params = {})
		files = params.fetch(:files)
		
		SevenZip.zip_files @tool, @args, @zip_name, files
	end
	
	def self.zip(params = {})
		tool = params.fetch(:tool).to_absolute
		args = params.fetch(:args, 'a')
		zip_name = params.fetch(:zip_name).to_absolute
		files = params.fetch(:files)
		
		zip_files tool, args, zip_name, files
	end
	
	def self.zip_files(tool, args, zip_name, files)
		return if files.empty?
    seven_zip = tool.to_absolute
    opts = [args,
            zip_name,
            files]
    sh seven_zip, *(opts)
	end
end

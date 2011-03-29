class TemplateFiles
  class << self
    def expand(template_path = {})
      Dir.glob("**/*.erb").map{|file_name| TemplateFile.new(file_name)}.each{|file| file.expand}
    end
  end
end

class TemplateFile
  def initialize(filename)
    @file = filename
  end

	def prepare_template(template)
		token_regex = /(@\w[\w\.\_]+\w@)/
		
		hits = template.scan(token_regex)
		tags = hits.map do |item|
      item[0].gsub(/@/,'').squeeze
		end
		
		tags.map! do |a|
			a.to_sym
		end
		tags.uniq!

		tags.inject(template) do |text, tag|
			text.gsub /@#{tag.to_s}@/, "<%= #{tag.to_s} %>"
		end
	end
		
	def process_template(template, args)
		b = binding
		erb = ERB.new(template, 0, "%")
		erb.result(b)
	end

  def expand(output_file = "",args={})
    template = prepare_template(File.read(@file))
    result = process_template(template,args)

		generated_file = output_file == "" ? @file.ext('') : output_file

    File.open_for_write(generated_file) do|file|
      file.write(result)
    end
  end
end

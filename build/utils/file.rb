class File
  def self.open_for_read(file)
     File.open(file,'r').each do|line|
       yield line
     end
  end

  def self.read_all_text(file)
    File.real_all_text_after_skipping_lines(file,0)
  end

  def self.real_all_text_after_skipping_lines(file,number_of_lines_to_skip)
    index = 1
    contents = ''
    File.open_for_read(file) do |line|
      contents += line if index > number_of_lines_to_skip
      index+=1
    end
    return contents
  end

  def self.delete?(file)
    File.delete(file) if File.exists?(file)
  end

  def self.open_for_write(file)
     File.open(file,'w') do|new_file|
       yield new_file
     end
  end
end

class File
  def self.open_for_read(file)
    File.open(file,'r').each do|line|
      yield line
    end
  end

  def self.open_for_write(file)
    File.open(file,'w') do|new_file|
      yield new_file
    end
  end

  def self.read_all_text(file)
    File.read_all_text_after_skipping_lines(file)
  end

  def self.read_all_text_after_skipping_lines(file, number_of_lines_to_skip=0)
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
end

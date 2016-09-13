class UniqueFiles
  attr_reader :unique_files

  def initialize(all_files)
    @unique_files = filter_unique(all_files)
  end

  def filter_unique(files)
    unique = {}

    files.each do|file|
      unique[File.basename(file)] = file
    end

    unique
  end

  def all_files
    return unique_files.values
  end
end

class String
  def join(elements = [])
    File.join(self,elements)
  end

  def as_win_path
    path = self.gsub(/\\/, "/")
    path = path.gsub(/\s/, "\\ ")
    path = path.gsub(/\(/, '\(')
    path = path.gsub(/\)/, '\)')
  end

  def win_path
    self.gsub(/\//, "\\") 
  end
end

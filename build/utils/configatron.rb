def delayed
  Configatron::Delayed.new do
    yield
  end
end
def dynamic
  Configatron::Dynamic.new do
    yield
  end
end

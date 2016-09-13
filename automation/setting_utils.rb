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


def missing(configuration_item_name,file)
  raise "You need to provide a value for #{configuration_item_name} in the file #{file}"
end

def potentially_change(configuration_item_name,file,&block)
  puts "********************You may want to consider changing the value of the setting #{configuration_item_name} in the file #{file}*********************************"
  delayed(&block)
end


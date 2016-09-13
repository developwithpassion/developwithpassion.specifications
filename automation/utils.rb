require_relative 'utils/string'
require_relative 'utils/file'
require_relative 'utils/unique_files'

module Automation
  module Utils
    extend self

    def pick_item_from(items, prompt)
      items.each_with_index{|item,index| p "#{index + 1} - #{item}"}
      index = ask(prompt).to_i
      return index == 0 ? "": items[index-1]
    end
  end
end


module Automation
  module Compile
    module CompileUnitResolution
      def get_compile_unit(file)
        proc = Proc.new {}
        result = eval(File.read(file), proc.binding)
        OpenStruct.new(result)
      end

      def load_compile_unit(unit)
        configure(compile_unit: unit)
      end
    end
  end
end

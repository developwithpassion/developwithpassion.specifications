module Automation
  module Compile
    class CSC
      attr_reader :compile_unit

      def self.run(unit)
        new(unit).run
      end

      def initialize(compile_unit)
        @compile_unit = compile_unit
      end

      def response_file
        @response_file ||= compile_unit.response_file
      end

      def run()
        automation = ::Automation::General.new
        configure(compile_unit: @compile_unit)
        automation.expand

        FileUtils.cp(compile_unit.base_response_template, response_file)
        system("csc @#{response_file}")
      end
    end
  end
end

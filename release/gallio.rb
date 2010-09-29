class Gallio

    attr_accessor :bin_path, :assembly_path, :report_path, :report_name
    
    def run()
        gallio = "#{binPath}/Gallio.Echo.exe " \
                   "/report-directory:\"#{@report_path}\" " \
                   "/report-name-format:\"#{@report_name}\" " \
                   "/report-type:Html " \
                   "\"#{assembly_path}\""

        sh gallio
    end
    
end

def gallio(*args, &block)
    body = lambda { |*args|
        gallio = Gallio.new
        block.call(gallio)
        gallio.run
    }
    Rake::Task.define_task(*args, &body)
end
    
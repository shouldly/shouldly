desc "Runs tests with NUnit"
task :test => [:compile] do
    tests = FileList["#{SOURCE_PATH}/Tests/**/Tests.dll"].exclude(/obj\//)
    sh "#{NUNIT_EXE} #{tests} /nologo /exclude=Pending /xml=#{OUTPUT_PATH}/UnitTestResults.xml"
end

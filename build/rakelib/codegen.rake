desc "Uses Tape to generate a single source file for NUnit.Framework"
task :tape_nunit do
    nunit_source_dir = File.expand_path Dir["#{LIB_PATH}/NUnit-*"][0]
    nunit_framework_autogen_source_file = "#{SOURCE_PATH}/Shouldly/NUnit/NUnitFramework.autogenerated.cs"
    sh "#{TAPE_EXE} #{nunit_source_dir}/src/NUnitFramework/framework #{nunit_framework_autogen_source_file} -i"

    header = "#pragma warning disable 3021 \n\n// This file was auto-generated by the tape_nunit rake task.\n// Do not modify the contents of this file manually"
    footer = "#pragma warning restore 3021"

    content = File.new(nunit_framework_autogen_source_file,'r').read
    source_content = "#{header} \n #{content} \n #{footer}"

    File.open(nunit_framework_autogen_source_file, 'w') { |fw| fw.write(source_content) }
end
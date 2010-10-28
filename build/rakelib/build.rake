desc "Generate assembly info."
assemblyinfo :version_assemblies => [:get_build_number] do |asm|
    asm.version = @@build_number
    asm.company_name = "Shouldly"
    asm.product_name = "Shouldly"
    asm.title = "Shouldly"
    asm.description = "Should style unit testing for .net"
    asm.copyright = "Copyright (c) 2010 Shouldly"
    asm.output_file = "#{SOURCE_PATH}/Shouldly/Properties/AssemblyInfo.cs"
end

desc "Builds the solution."
msbuild :compile => :version_assemblies do |msb|
    msb.properties :configuration => :Debug
    msb.targets :Clean, :Build
    msb.solution = "#{BASE_DIR}/Shouldly2010.sln"
end
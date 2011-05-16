desc "Generate assembly info."
assemblyinfo :version_assemblies => [:get_build_number] do |asm|
    asm.version = @@build_number
    asm.company_name = PROJECT_NAME
    asm.product_name = PROJECT_NAME
    asm.title = PROJECT_NAME
    asm.description = PROJECT_TAGLINE
    asm.copyright = "Copyright (c) 2010 #{PROJECT_NAME}"
    asm.output_file = "#{SOURCE_PATH}/Shouldly/Properties/AssemblyInfo.cs"
end

desc "Builds the solution."
msbuild :compile => :version_assemblies do |msb|
    msb.properties :configuration => :Debug
    msb.targets :Clean, :Build
    msb.solution = "#{BASE_DIR}/Shouldly2010.sln"
end
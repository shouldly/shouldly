desc "Gets build number based on git tags and commit."
task :get_build_number do
    version_info = JSON.parse(get_build_version)
    @@build_number = "#{version_info['NugetVersion']}"
    @@assembly_version = "#{version_info['Major']}.#{version_info['Minor']}.#{version_info['Patch']}"
    puts "Version number will be #{version_info['NugetVersion']}"
end

def get_build_version
    gitflowversion = File.expand_path "../lib/GitVersion/GitFlowVersion.exe"
    `#{gitflowversion}`
end

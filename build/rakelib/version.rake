desc "Gets build number based on git tags and commit."
task :get_build_number do
    version_info = JSON.parse(get_build_version)
    @@build_number = "#{version_info['NuGetVersion']}"
    @@assembly_version = "#{version_info['Major']}.#{version_info['Minor']}.#{version_info['Patch']}"
    puts "Version number will be #{version_info['NuGetVersion']}"
end

def get_build_version
    gitflowversion = File.expand_path "../lib/GitVersion/GitVersion.exe"
    `#{gitflowversion}`
end

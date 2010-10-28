desc "Gets build number based on git tags and commit."
task :get_build_number do
 	version_info = get_build_version
	@@build_number = "#{version_info[1]}.#{version_info[2]}.#{version_info[3]}.#{version_info[4]}"
end

def get_build_version
  /v(\d+)\.(\d+)\.(\d+)\-(\d+)/.match(`git describe --tags --long --match v*`.chomp)
end

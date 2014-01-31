task :default => ["clean", "all"]
task :all => [:compile, :test, :prepare_artefacts]

desc "Update assembly versions, build, generate docs and create directory for packaging"
task :deploy => [:version_assemblies, :default, :prepare_artefacts, :deploy_artefacts]

task :automated_deploy => [:get_build_number] do
	puts "##teamcity[buildNumber '#{@@build_number}']"
	
	puts "Building version v" + @@build_number

	# Hack for TeamCity's Git module which explicitly fetches --no-tags and screws our versioning scheme
	sh "ruby --version"
	sh "git.exe --version"
	sh "git.exe fetch --tags"

	Rake::Task["deploy"].invoke
end
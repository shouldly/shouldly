task :default => ["clean", "all"]
task :all => [:compile, :test]

desc "Update assembly versions, build, generate docs and create directory for packaging"
task :deploy => [:version_assemblies, :default, :prepare_artefacts] # :push_gem

task :automated_deploy do
	# Hack for TeamCity's Git module which explicitly fetches --no-tags and screws our versioning scheme
	sh "ruby --version"
	sh "git.exe --version"
	sh "git.exe fetch --tags"

	Rake::Task["deploy"].invoke
end
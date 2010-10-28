task :default => ["clean", "all"]
task :all => [:compile, :test]

desc "Update assembly versions, build, generate docs and create directory for packaging"
task :deploy => [:version_assemblies, :default, :nupack, :create_gem] # :push_gem

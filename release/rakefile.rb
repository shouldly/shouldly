require "albacore"
require "release/common"
require "release/gallio"
require "rubygems"
require "rake/gempackagetask"

task :default => [:deploySample]

desc "Inits the build"
task :initBuild do
	Common.EnsurePath("reports")
end

desc "Generate assembly info."
assemblyinfo :assemblyInfo => :initBuild do |asm|
    asm.version = ENV["GO_PIPELINE_LABEL"]
    asm.company_name = "Shouldly"
    asm.product_name = "Shouldly"
    asm.title = "Shouldly"
    asm.description = "Should testing for .net"
    asm.copyright = "Copyright (c) 2010 Shouldly"
    asm.output_file = "src/Shouldly/Properties/AssemblyInfo.cs"
end

desc "Builds the solution."
msbuild :build => :assemblyInfo do |msb|
    msb.path_to_command = File.join(ENV['windir'], 'Microsoft.NET', 'Framework', 'v4.0.30319', 'MSBuild.exe')
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "Shouldly2010.sln"
end

desc "Gallio Test Runner"
gallio :test => :build do |gallio|
    gallio.bin_path = 'C:/Program Files/Gallio/bin'
	gallio.assembly_path = 'src/Tests/bin/Release/Tests.dll'
	gallio.report_path = 'reports'
	gallio.report_name = 'index'
end

desc "Prepares the files to be deployed."
task :prepareDeploy => :test do
    
    deploy = "deploy"
    lib = "#{deploy}/files/lib"
    docs = "#{deploy}/files/docs"
    pkg = "#{deploy}/pkg"
    
	Common.DeleteDirectory(deploy)
	
    Common.EnsurePath(lib)
    Common.EnsurePath(pkg)
    Common.EnsurePath(docs)
    
	Common.CopyFiles("src/Shouldly/bin/Release/Shouldly.*", lib) 
	Common.CopyFiles("src/docs/**/*", docs) 

end

desc "Zips and eploys the application binaries."
zip :deployBinaries => :prepareDeploy do |zip|
    zip.directories_to_zip 'deploy/files/lib'
    zip.output_file = "Shouldly_#{ENV['GO_PIPELINE_LABEL']}.zip"
    zip.output_path = 'D:/Websites/public.mikeobrien.net/wwwroot/Releases/Shouldly'
end

desc "Creates the gem"
task :createGem => :deployBinaries do

    FileUtils.cd("gem/files") do
    
        spec = Gem::Specification.new do |spec|
            spec.platform = Gem::Platform::RUBY
            spec.summary = "Should testing for .net"
            spec.name = "shouldly"
            spec.version = "#{ENV['GO_PIPELINE_LABEL']}"
            spec.files = Dir["lib/**/*"] + Dir["docs/**/*"]
			spec.add_runtime_dependency("nunit", ">= 2.5.3.9345")
			spec.add_runtime_dependency("rhino.mocks", ">= 3.6.0.0")
            spec.authors = ["Xerxes Battiwalla"]
            spec.homepage = "http://github.com/shouldly/shouldly/"
            spec.description = "Should testing for .net"
        end

        Rake::GemPackageTask.new(spec) do |package|
            package.package_dir = "../pkg"
        end
        
        Rake::Task["package"].invoke
    end
end

desc "Pushes the gem to the ruby gems server"
task :pushGem => :createGem do
	result = system("gem", "push", "gem/pkg/shouldly-#{ENV['GO_PIPELINE_LABEL']}.gem")
end

#desc "Tag the current release"
#task :tagRelease do
#	result = system("git", "tag", "-a", "v#{ENV['GO_PIPELINE_LABEL']}", "-m", "release-v#{ENV['GO_PIPELINE_LABEL']}")
#	result = system("git", "push", "--tags")
#end


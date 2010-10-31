desc "Create NuPack package"
task :nupack => [:collate_package_contents] do
	output_base_path = "#{OUTPUT_PATH}/#{CONFIG}"
	dll_path = "#{output_base_path}/#{PROJECT_NAME}"
	deploy_path = "#{output_base_path}/#{PROJECT_NAME}-#{@@build_number}"
	nupack_path = "#{output_base_path}/nupack/#{@@build_number}"
	nupack_lib_path = "#{output_base_path}/nupack/#{@@build_number}/lib/35"

    #Ensure nupack path exists
    mkdir_p nupack_lib_path

    #Copy binaries into lib path
    cp Dir.glob("#{dll_path}/*.{dll,xml}"), nupack_lib_path

    #Copy nuspec and *.txt docs into package root
    cp Dir.glob("#{deploy_path}/*.txt"), nupack_path
    cp "#{PROJECT_NAME}.nuspec", nupack_path
    updateNuspec("#{nupack_path}/#{PROJECT_NAME}.nuspec")

    #Build package
    full_path_to_nupack_exe = File.expand_path(NUPACK_EXE, File.dirname(__FILE__))
    nuspec = File.expand_path("#{nupack_path}/#{PROJECT_NAME}.nuspec", File.dirname(__FILE__))
    FileUtils.cd "#{output_base_path}/nupack" do
        sh "#{full_path_to_nupack_exe} #{nuspec}"
    end
end

desc "Creates the gem"
task :create_gem => [:collate_package_contents] do
	output_base_path = "#{OUTPUT_PATH}/#{CONFIG}"
	deploy_path = "#{output_base_path}/#{PROJECT_NAME}-#{@@build_number}"
	gem_path = "#{output_base_path}/gem"

    #Ensure gem path exists
    mkdir_p gem_path
    mkdir_p "#{gem_path}/files"
    mkdir_p "#{gem_path}/files/lib"
    mkdir_p "#{gem_path}/files/doc"
    mkdir_p "#{gem_path}/pkg"

    Dir.glob("src/WcfRestContrib/bin/Release/*") do |name|
        FileUtils.cp(name, lib)
    end    
    
    Dir.glob("src/docs/**/*") do |name|
        FileUtils.cp(name, docs)
    end    	
	
    #Copy content into gem root
    cp Dir.glob("#{deploy_path}/*.{xml,dll}"), "#{gem_path}/files/lib"
    cp Dir.glob("#{deploy_path}/*.txt"), "#{gem_path}/files/doc"

    FileUtils.cd("#{gem_path}/files") do
        spec = Gem::Specification.new do |spec|
            spec.platform = Gem::Platform::RUBY
            spec.summary = PROJECT_TAGLINE
            spec.name = "shouldly"
            spec.version = "#{@@build_number}"
            spec.files = Dir["lib/**/*"] + Dir["docs/**/*"]
			spec.add_runtime_dependency("nunit", ">= 2.5.3.9345")
			spec.add_runtime_dependency("rhino.mocks", ">= 3.6.0.0")
            spec.authors = ["Dave Newman", "Peter van der Woude", "Anthony Egerton", "Xerxes Battiwalla"]
            spec.homepage = "http://shouldly.github.com/"
            spec.description = PROJECT_TAGLINE
        end

        Rake::GemPackageTask.new(spec) do |package|
            package.package_dir = "../pkg"
        end
        
        Rake::Task["package"].invoke
    end
end

desc "Creates the .ZIP package for the release build"
task :create_zip => [:collate_package_contents] do
	output_base_path = "#{OUTPUT_PATH}/#{CONFIG}"
	
	zip_path = "#{output_base_path}/zip/"
    mkdir_p zip_path

	sh "#{ZIP_EXE} -r -j #{zip_path}/#{PROJECT_NAME}-#{@@build_number}.zip #{output_base_path}/#{PROJECT_NAME}-#{@@build_number}"
end

desc "Pushes the gem to the ruby gems server"
task :push_gem => :create_gem do
	gem_path = "#{output_base_path}/gem"
	result = system("gem", "push", "gem/pkg/shouldly-#{@@build_number}.gem")
end

task :collate_package_contents => [:get_build_number] do
	output_base_path = "#{OUTPUT_PATH}/#{CONFIG}"
	dll_path = output_base_path 
	# "#{output_base_path}/#{PROJECT_NAME}"
	deploy_path = "#{output_base_path}/#{PROJECT_NAME}-#{@@build_number}"

    mkdir_p deploy_path
	cp Dir.glob("#{dll_path}/*.{dll,xml}"), deploy_path

	cp "../README.markdown", "#{deploy_path}/README.txt"
	cp "../LICENSE.txt", "#{deploy_path}"

    tidyUpTextFileFromMarkdown("#{deploy_path}/README.txt")
end



def updateNuspec(file)
    text = File.read(file)
    modified_date = DateTime.now.rfc3339
    text.gsub! /<version>.*?<\/version>/, "<version>#{@@build_number}</version>"
    text.gsub! /<modified>.*?<\/modified>/, "<modified>#{modified_date}</modified>"
    File.open(file, 'w') { |f| f.write(text) }
end

def tidyUpTextFileFromMarkdown(file)
    text = File.read(file)
    File.open(file, "w") { |f| f.write( stripHtmlComments(text) ) }
end

def stripHtmlComments(text)
    startComment = "<!--"
    endComment = "-->"

    indexOfStart = text.index(startComment)
    indexOfEnd = text.index(endComment)
    return text if indexOfStart.nil? or indexOfEnd.nil?

    text[indexOfStart..(indexOfEnd+endComment.length-1)] = ""
    return stripHtmlComments(text)
end

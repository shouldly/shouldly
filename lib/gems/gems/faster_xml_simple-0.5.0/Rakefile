require 'rubygems'
require 'rake'
require 'rake/testtask'
require 'rake/rdoctask'
require 'rake/packagetask'
require 'rake/gempackagetask'
require 'lib/faster_xml_simple'

task :default => :test

Rake::TestTask.new do |test|
  test.pattern = 'test/*_test.rb'
  test.verbose = true
end


Rake::RDocTask.new do |rdoc|  
  rdoc.rdoc_dir = 'doc'  
  rdoc.title    = "FasterXmlSimple, a libxml based replacement for XmlSimple"  
  rdoc.options << '--line-numbers' << '--inline-source'
  rdoc.rdoc_files.include('README')
  rdoc.rdoc_files.include('COPYING')
  rdoc.rdoc_files.include('lib/**/*.rb')
end

namespace :dist do  

  spec = Gem::Specification.new do |s|
    s.name              = 'faster_xml_simple'
    s.version           = Gem::Version.new(FasterXmlSimple::Version)
    s.summary           = "A libxml based replacement for XmlSimple"
    s.description       = s.summary
    s.email             = 'michael@koziarski.com'
    s.author            = 'Michael Koziarski'
    s.has_rdoc          = true
    s.extra_rdoc_files  = %w(README COPYING)
    s.homepage          = 'http://fasterxs.rubyforge.org'
    s.rubyforge_project = 'fasterxs'
    s.files             = FileList['Rakefile', 'lib/**/*.rb']
    s.test_files        = Dir['test/**/*']

    s.add_dependency 'libxml-ruby', '>= 0.3.8.4'
    s.rdoc_options  = ['--title', "",
                       '--main',  'README',
                       '--line-numbers', '--inline-source']
  end
  Rake::GemPackageTask.new(spec) do |pkg|
    pkg.need_tar_gz = true
    pkg.package_files.include('{lib,test}/**/*')
    pkg.package_files.include('README')
    pkg.package_files.include('COPYING')
    pkg.package_files.include('Rakefile')
  end
end
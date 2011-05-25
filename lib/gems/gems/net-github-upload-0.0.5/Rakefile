# vim: fileencoding=utf-8
require 'rubygems'
require 'rake'
require 'rake/clean'
require 'rake/testtask'
require 'rake/packagetask'
require 'rake/gempackagetask'
require 'rake/rdoctask'
require 'rake/contrib/sshpublisher'
require './lib/net/github-upload'

$version = Net::GitHub::Upload::VERSION
$readme = 'README.rdoc'
$rdoc_opts = %W(--main #{$readme} --charset utf-8 --line-numbers --inline-source)
$name = 'net-github-upload'
$github_name = 'ruby-net-github-upload'
$summary = 'ruby porting of Net::GitHub::Upload'
$description = <<-EOS
Ruby Net::GitHub::Upload is upload user agent for GitHub Downloads
EOS
$author = 'Constellation'
$email = 'utatane.tea@gmail.com'
$page = 'http://github.com/Constellation/ruby-net-github-upload'
$rubyforge_project = 'ruby-net-github-upload'


task :default => [:test]
task :package => [:clean]

Rake::TestTask.new("test") do |t|
  t.libs << "test"
  t.pattern = "test/**/*_test.rb"
  t.verbose = true
end

spec = Gem::Specification.new do |s|
  s.name = $name
  s.version = $version
  s.platform = Gem::Platform::RUBY
  s.has_rdoc = true
  s.extra_rdoc_files = [$readme]
  s.rdoc_options += $rdoc_opts
  s.summary = $summary
  s.description = $description
  s.author = $author
  s.email = $email
  s.homepage = $page
  s.executables = $exec
  s.rubyforge_project = $rubyforge_project
  s.require_path = 'lib'
  s.test_files = Dir["test/*_test.rb"]
  s.add_dependency('nokogiri', '>=1.4.0')
  s.add_dependency('faster_xml_simple')
  s.add_dependency('json')
  s.add_dependency('httpclient')
  s.files = %w(README.rdoc Rakefile) + Dir["{bin,test,lib}/**/*"]
end

Rake::GemPackageTask.new(spec) do |p|
  p.need_tar = true
  p.gem_spec = spec
end

Rake::RDocTask.new do |rdoc|
  rdoc.rdoc_dir = 'doc'
  rdoc.options += $rdoc_opts
  rdoc.rdoc_files.include("README.rdoc", "lib/**/*.rb", "ext/**/*.c")
end

desc "gem spec"
task :gemspec do
  File.open("#{$github_name}.gemspec", "wb") do |f|
    f << spec.to_ruby
  end
end

desc "gem install"
task :install => [:package] do
  sh "gem install pkg/#{$name}-#{$version}.gem --local"
end

desc "gem uninstall"
task :uninstall do
  sh "gem uninstall #{$name}"
end

# vim: syntax=ruby

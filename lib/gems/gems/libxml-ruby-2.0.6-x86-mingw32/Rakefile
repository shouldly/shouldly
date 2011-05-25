#!/usr/bin/env ruby

require "rubygems"
require "rake/extensiontask"
require "rake/testtask"
require 'hanna/rdoctask'
require "grancher/task"
require "yaml"

GEM_NAME = "libxml-ruby"
SO_NAME  = "libxml_ruby"

# Read the spec file
spec = Gem::Specification.load("#{GEM_NAME}.gemspec")

# Setup compile tasks
Rake::ExtensionTask.new do |ext|
  ext.gem_spec = spec
  ext.name = SO_NAME
  ext.ext_dir = "ext/libxml"
  ext.lib_dir = "lib/#{RUBY_VERSION.sub(/\.\d$/, '')}"
  ext.config_options << "--with-xml2-include=C:/MinGW/local/include/libxml2"
end

# Setup generic gem
Rake::GemPackageTask.new(spec) do |pkg|
  pkg.package_dir = 'pkg'
  pkg.need_tar    = false
end

# Setup Windows Gem
if RUBY_PLATFORM.match(/win32|mingw32/)
  binaries = (FileList['lib/**/*.so',
                       'lib/**/*dll'])

  # Windows specification
  win_spec = spec.clone
  win_spec.platform = Gem::Platform::CURRENT
  win_spec.files += binaries.to_a

  # Unset extensions
  win_spec.extensions = nil

  # Rake task to build the windows package
  Rake::GemPackageTask.new(win_spec) do |pkg|
    pkg.package_dir = 'pkg'
    pkg.need_tar = false
  end
end

# RDoc Task
desc "Generate rdoc documentation"
Rake::RDocTask.new("rdoc") do |rdoc|
  rdoc.rdoc_dir = 'doc/libxml-ruby/rdoc'
  rdoc.title    = "LibXML"
  # Show source inline with line numbers
  rdoc.options << "--line-numbers"
  # Make the readme file the start page for the generated html
  rdoc.main = 'README.rdoc'
  rdoc.rdoc_files.include('doc/*.rdoc',
                          'ext/**/libxml.c',
                          'ext/**/ruby_xml.c',
                          'ext/**/*.c',
                          'lib/**/*.rb',
                          'README.rdoc',
                          'HISTORY',
                          'LICENSE')
end

# Test Task
Rake::TestTask.new do |t|
  t.libs << "test"
  t.verbose = true
end

# Publish Website to Github
Grancher::Task.new do |g|
  # push gh-pages
  g.branch  = 'gh-pages'
  # to origin
  g.push_to = 'origin'
  # copy the website directory
  g.directory 'web'
  # and the rdoc directory
  g.directory 'doc/libxml-ruby/rdoc', 'rdoc'
end

desc "Build docs, and publish the website"
task :publish_with_docs => [:rdoc, :publish]

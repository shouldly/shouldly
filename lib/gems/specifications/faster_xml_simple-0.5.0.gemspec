# -*- encoding: utf-8 -*-

Gem::Specification.new do |s|
  s.name = %q{faster_xml_simple}
  s.version = "0.5.0"

  s.required_rubygems_version = nil if s.respond_to? :required_rubygems_version=
  s.authors = ["Michael Koziarski"]
  s.cert_chain = nil
  s.date = %q{2006-12-23}
  s.description = %q{A libxml based replacement for XmlSimple}
  s.email = %q{michael@koziarski.com}
  s.extra_rdoc_files = ["README", "COPYING"]
  s.files = ["Rakefile", "lib/faster_xml_simple.rb", "README", "COPYING", "test/fixtures", "test/regression_test.rb", "test/test_helper.rb", "test/xml_simple_comparison_test.rb", "test/fixtures/test-1.rails.yml", "test/fixtures/test-1.xml", "test/fixtures/test-1.yml", "test/fixtures/test-2.rails.yml", "test/fixtures/test-2.xml", "test/fixtures/test-2.yml", "test/fixtures/test-3.rails.yml", "test/fixtures/test-3.xml", "test/fixtures/test-3.yml", "test/fixtures/test-4.rails.yml", "test/fixtures/test-4.xml", "test/fixtures/test-4.yml", "test/fixtures/test-5.rails.yml", "test/fixtures/test-5.xml", "test/fixtures/test-5.yml", "test/fixtures/test-6.rails.yml", "test/fixtures/test-6.xml", "test/fixtures/test-6.yml", "test/fixtures/test-7.rails.yml", "test/fixtures/test-7.xml", "test/fixtures/test-7.yml", "test/fixtures/test-8.rails.yml", "test/fixtures/test-8.xml", "test/fixtures/test-8.yml"]
  s.homepage = %q{http://fasterxs.rubyforge.org}
  s.rdoc_options = ["--title", "", "--main", "README", "--line-numbers", "--inline-source"]
  s.require_paths = ["lib"]
  s.required_ruby_version = Gem::Requirement.new("> 0.0.0")
  s.rubyforge_project = %q{fasterxs}
  s.rubygems_version = %q{1.5.2}
  s.summary = %q{A libxml based replacement for XmlSimple}
  s.test_files = ["test/fixtures", "test/regression_test.rb", "test/test_helper.rb", "test/xml_simple_comparison_test.rb", "test/fixtures/test-1.rails.yml", "test/fixtures/test-1.xml", "test/fixtures/test-1.yml", "test/fixtures/test-2.rails.yml", "test/fixtures/test-2.xml", "test/fixtures/test-2.yml", "test/fixtures/test-3.rails.yml", "test/fixtures/test-3.xml", "test/fixtures/test-3.yml", "test/fixtures/test-4.rails.yml", "test/fixtures/test-4.xml", "test/fixtures/test-4.yml", "test/fixtures/test-5.rails.yml", "test/fixtures/test-5.xml", "test/fixtures/test-5.yml", "test/fixtures/test-6.rails.yml", "test/fixtures/test-6.xml", "test/fixtures/test-6.yml", "test/fixtures/test-7.rails.yml", "test/fixtures/test-7.xml", "test/fixtures/test-7.yml", "test/fixtures/test-8.rails.yml", "test/fixtures/test-8.xml", "test/fixtures/test-8.yml"]

  if s.respond_to? :specification_version then
    s.specification_version = 1

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
      s.add_runtime_dependency(%q<libxml-ruby>, [">= 0.3.8.4"])
    else
      s.add_dependency(%q<libxml-ruby>, [">= 0.3.8.4"])
    end
  else
    s.add_dependency(%q<libxml-ruby>, [">= 0.3.8.4"])
  end
end

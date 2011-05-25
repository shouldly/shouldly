# -*- encoding: utf-8 -*-

Gem::Specification.new do |s|
  s.name = %q{net-github-upload}
  s.version = "0.0.5"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.authors = ["Constellation"]
  s.date = %q{2010-11-11}
  s.description = %q{Ruby Net::GitHub::Upload is upload user agent for GitHub Downloads
}
  s.email = %q{utatane.tea@gmail.com}
  s.extra_rdoc_files = ["README.rdoc"]
  s.files = ["README.rdoc", "Rakefile", "test/github_test.rb", "test/test", "lib/net/github-upload.rb"]
  s.homepage = %q{http://github.com/Constellation/ruby-net-github-upload}
  s.rdoc_options = ["--main", "README.rdoc", "--charset", "utf-8", "--line-numbers", "--inline-source"]
  s.require_paths = ["lib"]
  s.rubyforge_project = %q{ruby-net-github-upload}
  s.rubygems_version = %q{1.5.2}
  s.summary = %q{ruby porting of Net::GitHub::Upload}
  s.test_files = ["test/github_test.rb"]

  if s.respond_to? :specification_version then
    s.specification_version = 3

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
      s.add_runtime_dependency(%q<nokogiri>, [">= 1.4.0"])
      s.add_runtime_dependency(%q<faster_xml_simple>, [">= 0"])
      s.add_runtime_dependency(%q<json>, [">= 0"])
      s.add_runtime_dependency(%q<httpclient>, [">= 0"])
    else
      s.add_dependency(%q<nokogiri>, [">= 1.4.0"])
      s.add_dependency(%q<faster_xml_simple>, [">= 0"])
      s.add_dependency(%q<json>, [">= 0"])
      s.add_dependency(%q<httpclient>, [">= 0"])
    end
  else
    s.add_dependency(%q<nokogiri>, [">= 1.4.0"])
    s.add_dependency(%q<faster_xml_simple>, [">= 0"])
    s.add_dependency(%q<json>, [">= 0"])
    s.add_dependency(%q<httpclient>, [">= 0"])
  end
end

# -*- encoding: utf-8 -*-

Gem::Specification.new do |s|
  s.name = %q{httpclient}
  s.version = "2.2.0.2"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.authors = ["NAKAMURA, Hiroshi"]
  s.date = %q{2011-04-25}
  s.email = %q{nahi@ruby-lang.org}
  s.files = ["lib/oauthclient.rb", "lib/http-access2.rbc", "lib/http-access2.rb", "lib/httpclient.rbc", "lib/http-access2/cookie.rb", "lib/http-access2/http.rb", "lib/hexdump.rb", "lib/httpclient/ssl_config.rbc", "lib/httpclient/cacert_sha1.p7s", "lib/httpclient/http.rbc", "lib/httpclient/cookie.rb", "lib/httpclient/session.rbc", "lib/httpclient/auth.rbc", "lib/httpclient/session.rb", "lib/httpclient/cookie.rbc", "lib/httpclient/timeout.rb", "lib/httpclient/util.rb", "lib/httpclient/cacert.p7s", "lib/httpclient/ssl_config.rb", "lib/httpclient/connection.rb", "lib/httpclient/auth.rb", "lib/httpclient/timeout.rbc", "lib/httpclient/connection.rbc", "lib/httpclient/util.rbc", "lib/httpclient/http.rb", "lib/httpclient.rb"]
  s.homepage = %q{http://github.com/nahi/httpclient}
  s.require_paths = ["lib"]
  s.rubygems_version = %q{1.5.2}
  s.summary = %q{gives something like the functionality of libwww-perl (LWP) in Ruby}

  if s.respond_to? :specification_version then
    s.specification_version = 3

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
    else
    end
  else
  end
end

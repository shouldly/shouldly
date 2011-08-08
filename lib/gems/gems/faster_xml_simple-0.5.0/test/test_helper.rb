
require 'test/unit'
require 'faster_xml_simple'

class FasterXSTest < Test::Unit::TestCase
  def default_test
  end
  
  def silence_stderr
    str = STDERR.dup
    STDERR.reopen("/dev/null")
    STDERR.sync=true
    yield
  ensure
    STDERR.reopen(str)
  end
end

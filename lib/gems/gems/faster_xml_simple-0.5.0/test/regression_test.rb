require File.dirname(__FILE__) + '/test_helper'

class RegressionTest < FasterXSTest
  def test_content_nil_regressions
    expected = {"asdf"=>{"jklsemicolon"=>{}}}
    assert_equal expected, FasterXmlSimple.xml_in("<asdf><jklsemicolon /></asdf>")
    assert_equal expected, FasterXmlSimple.xml_in("<asdf><jklsemicolon /></asdf>", 'forcearray'=>['asdf'])
  end
  
  def test_s3_regression
    str = File.read("test/fixtures/test-7.xml")
    assert_nil FasterXmlSimple.xml_in(str)["AccessControlPolicy"]["AccessControlList"]["__content__"]
  end
  
  def test_xml_simple_transparency
    assert_equal XmlSimple.xml_in("<asdf />"), FasterXmlSimple.xml_in("<asdf />")
  end
  
  def test_suppress_empty_variations
    str = "<asdf><fdsa /></asdf>"
    
    assert_equal Hash.new, FasterXmlSimple.xml_in(str)["asdf"]["fdsa"]
    assert_nil FasterXmlSimple.xml_in(str, 'suppressempty'=>nil)["asdf"]["fdsa"]
    assert_equal '', FasterXmlSimple.xml_in(str, 'suppressempty'=>'')["asdf"]["fdsa"]
    assert !FasterXmlSimple.xml_in(str, 'suppressempty'=>true)["asdf"].has_key?("fdsa")
  end

  def test_empty_string_doesnt_crash
    assert_raise(XML::Parser::ParseError) do
      silence_stderr do 
        FasterXmlSimple.xml_in('')
      end
    end
  end
  
  def test_keeproot_false
    str = "<asdf><fdsa>1</fdsa></asdf>"
    expected = {"fdsa"=>"1"}
    assert_equal expected, FasterXmlSimple.xml_in(str, 'keeproot'=>false)
  end
  
  def test_keeproot_false_with_force_content
    str = "<asdf><fdsa>1</fdsa></asdf>"
    expected = {"fdsa"=>{"__content__"=>"1"}}
    assert_equal expected, FasterXmlSimple.xml_in(str, 'keeproot'=>false, 'forcecontent'=>true)
  end
end
# encoding: UTF-8

require './test_helper'
require 'test/unit'

class TestSchema < Test::Unit::TestCase
  def setup
    file = File.join(File.dirname(__FILE__), 'model/shiporder.xml')
    @doc = XML::Document.file(file)
  end
  
  def teardown
    @doc = nil
  end
  
  def schema
    document = XML::Document.file(File.join(File.dirname(__FILE__), 'model/shiporder.xsd'))
    XML::Schema.document(document)
  end

  def test_from_doc
    assert_instance_of(XML::Schema, schema)
  end
  
  def test_valid
    assert(@doc.validate_schema(schema))
  end
  
  def test_invalid
    new_node = XML::Node.new('invalid', 'this will mess up validation')
    @doc.root << new_node

    error = assert_raise(XML::Error) do
      @doc.validate_schema(schema)
    end

    assert_not_nil(error)
    assert_kind_of(XML::Error, error)
    assert(error.message.match(/Error: Element 'invalid': This element is not expected. Expected is \( item \)/))
    assert_equal(XML::Error::SCHEMASV, error.domain)
    assert_equal(XML::Error::SCHEMAV_ELEMENT_CONTENT, error.code)
    assert_equal(XML::Error::ERROR, error.level)
    assert(error.file.match(/shiporder.xml/))
    assert_nil(error.line)
    assert_nil(error.str1)
    assert_nil(error.str2)
    assert_nil(error.str3)
    assert_equal(0, error.int1)
    assert_equal(0, error.int2)
    assert_not_nil(error.node)
    assert_equal('invalid', error.node.name)
  end
end

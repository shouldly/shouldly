# encoding: UTF-8

require './test_helper'
require "tempfile"
require "test/unit"

class TestXPathContext < Test::Unit::TestCase
  SOAP_PREFIX = 'soap'
  SOAP_URI = 'http://schemas.xmlsoap.org/soap/envelope/'
  
  NS0_PREFIX = 'ns0'
  NS0_URI = 'http://services.somewhere.com'
  
  def setup
    doc = XML::Document.file(File.join(File.dirname(__FILE__), 'model/soap.xml'))
    @context = XML::XPath::Context.new(doc)
  end
  
  def teardown()
    @context = nil
  end
  
  #def test_no_ns
    #assert_raise(XML::XPath::InvalidPath) do
      #@context.find('/soap:Envelope')
    #end
  #end    

  def test_ns_register
    @context.register_namespace(SOAP_PREFIX, SOAP_URI)
    @context.register_namespace(NS0_PREFIX, NS0_URI)
    nodes = @context.find('/soap:Envelope/soap:Body/ns0:getManufacturerNamesResponse')
    assert_equal(1, nodes.length)
  end
      
  def test_ns_register_string
    @context.register_namespaces("#{SOAP_PREFIX}:#{SOAP_URI}")
    @context.register_namespaces("#{NS0_PREFIX}:#{NS0_URI}")
    nodes = @context.find('/soap:Envelope/soap:Body/ns0:getManufacturerNamesResponse')
    assert_equal(1, nodes.length)
  end
      
  def test_ns_register_array
    @context.register_namespaces(["#{SOAP_PREFIX}:#{SOAP_URI}", "#{NS0_PREFIX}:#{NS0_URI}"])
    nodes = @context.find('/soap:Envelope/soap:Body/ns0:getManufacturerNamesResponse')
    assert_equal(1, nodes.length)
  end
  
  def test_ns_register_hash
    @context.register_namespaces(SOAP_PREFIX => SOAP_URI,
                                 NS0_PREFIX => NS0_URI)

    nodes = @context.find('/soap:Envelope/soap:Body/ns0:getManufacturerNamesResponse')
    assert_equal(1, nodes.length)
  end

  def test_ns_register_node
    @context.register_namespaces_from_node(@context.doc.root)
    nodes = @context.find('/soap:Envelope')
    assert_equal(1, nodes.length)
  end
  
  def test_node
    @context.register_namespaces_from_node(@context.doc.root)
    
    nodes = @context.find('soap:Body')
    assert_equal(0, nodes.length)
    
    
    @context.node = @context.doc.root.child.next
    nodes = @context.find('soap:Body')
    assert_equal(0, nodes.length)
  end

  def test_cache
    @context.enable_cache
    @context.enable_cache(10)
    @context.disable_cache
  end
end

# encoding: UTF-8

require './test_helper'
require 'test/unit'

class TestTranversal < Test::Unit::TestCase
  ROOT_NODES_LENGTH = 25
  ROOT_ELEMENTS_LENGTH = 12
  
  def setup
    filename = File.join(File.dirname(__FILE__), 'model/books.xml')
    @doc = XML::Document.file(filename)
  end
  
  def teardown
    @doc = nil
  end
  
  def test_children
    # Includes text nodes and such
    assert_equal(ROOT_NODES_LENGTH, @doc.root.children.length)
  end
  
  def test_children_iteration
    # Includes text nodes and such
    nodes = @doc.root.children.inject([]) do |arr, node|
      arr << node
      arr
    end
    
    assert_equal(ROOT_NODES_LENGTH, nodes.length)
  end
  
  def test_each
    # Includes text nodes and such
    nodes = @doc.root.inject([]) do |arr, node|
      arr << node
      arr
    end
    
    assert_equal(ROOT_NODES_LENGTH, nodes.length)
  end
  
  def test_each_element
    # Includes text nodes and such
    nodes = []
    @doc.root.each_element do |node|
      nodes << node
    end
    
    assert_equal(ROOT_ELEMENTS_LENGTH, nodes.length)
  end
  
  def test_next
    nodes = []
    
    node = @doc.root.first
    
    while node
      nodes << node
      node = node.next
    end
    assert_equal(ROOT_NODES_LENGTH, nodes.length)
  end

  def test_next?
    first_node = @doc.root.first
    assert(first_node.next?)
    
    last_node = @doc.root.last
    assert(!last_node.next?)
  end
    
  def test_prev
    nodes = []
    
    node = @doc.root.last
    
    while node
      nodes << node
      node = node.prev
    end
    assert_equal(ROOT_NODES_LENGTH, nodes.length)
  end
  
  def test_prev?
    first_node = @doc.root.first
    assert(!first_node.prev?)
    
    last_node = @doc.root.last
    assert(last_node.prev?)
  end

  def test_parent?
    assert(!@doc.parent?)
    assert(@doc.root.parent?)
  end
   
  def test_child?
    assert(@doc.child?)
    assert(!@doc.root.first.child?)
  end
  
  def test_next_prev_equivalence
    next_nodes = []
    last_nodes = []
    
    node = @doc.root.first
    while node
      next_nodes << node
      node = node.next
    end
  
    node = @doc.root.last
    while node
      last_nodes << node
      node = node.prev
    end
    
    assert_equal(next_nodes, last_nodes.reverse)
  end
  
  def test_next_children_equivalence
    next_nodes = []
    
    node = @doc.root.first
    while node
      next_nodes << node
      node = node.next
    end
  
    assert_equal(@doc.root.children, next_nodes)
  end
  
  #    node = @doc.find_first('book')
   # node.children.each do |node|
    #  puts 1
    #end
   # @doc.root.children do |node|
    #  if node.node_type == XML::Node::ELEMENT_NODE
     #   puts node.name
      #end
    #end
    #assert_equal(12,nodes.length)
  #end

  #def test_doc_class
    #assert_instance_of(XML::Document, @doc)
  #end
  
  #def test_root_class
    #assert_instance_of(XML::Node, @doc.root)
  #end
  
  #def test_node_class
    #for n in nodes
      #assert_instance_of(XML::Node, n)
    #end
  #end

  #def test_find_class
    #set = @doc.find('/ruby_array/fixnum')
    #assert_instance_of(XML::XPath::Object, set)
  #end

  #def test_node_child_get
    #assert_instance_of(TrueClass, @doc.root.child?)
    #assert_instance_of(XML::Node, @doc.root.child)
    #assert_equal('fixnum', @doc.root.child.name)
  #end

  #def test_node_doc
    #for n in nodes
      #assert_instance_of(XML::Document, n.doc) if n.document?
    #end
  #end

  #def test_node_type_name
    #assert_equal('element', nodes[0].node_type_name)
    #assert_equal('element', nodes[1].node_type_name)
  #end

  #def test_node_find
    #set = @doc.root.find('./fixnum').set
    #assert_instance_of(XML::Node::Set, set)
    #for node in set
      #assert_instance_of(XML::Node, node)
    #end
  #end
  
  #def test_equality
    #node_a = @doc.find('/ruby_array/fixnum').first
    #node_b = @doc.root.child
    #assert(node_a == node_b)
    #assert(node_a.eql?(node_b))
    #assert(node_a.equal?(node_b))
    
    #xp2 = XML::Parser.new()
    #xp2.string = '<ruby_array uga="booga" foo="bar"><fixnum>one</fixnum><fixnum>two</fixnum></ruby_array>'
    #doc2 = xp2.parse

    #node_a2 = doc2.find('/ruby_array/fixnum').first
    
    #assert(node_a.to_s == node_a2.to_s)
    #assert(node_a == node_a2)
    #assert(node_a.eql?(node_a2))
    #assert(!node_a.equal?(node_a2))
  #end
      
  #def test_content()
    #assert_equal('onetwo', @doc.root.content)
    
    #first = @doc.root.child
    #assert_equal('one', first.content)
    #assert_equal('two', first.next.content)
  #end
  
  #def test_base
    #doc = XML::Parser.string('<person />').parse
    #assert_nil(doc.root.base)
  #end
end

# 
# Copyright (c) 2006 Michael Koziarski
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy of
# this software and associated documentation files (the "Software"), to deal in the
# Software without restriction, including without limitation the rights to use,
# copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
# Software, and to permit persons to whom the Software is furnished to do so,
# subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
# FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
# COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
# AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
# WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

require 'rubygems'
require 'xml/libxml'

class FasterXmlSimple
  Version = '0.5.0'
  class << self
    # Take an string containing XML, and returns a hash representing that 
    # XML document.  For example:
    # 
    #   FasterXmlSimple.xml_in("<root><something>1</something></root>")
    #   {"root"=>{"something"=>{"__content__"=>"1"}}}
    #
    # Faster XML Simple is designed to be a drop in replacement for the xml_in
    # functionality of http://xml-simple.rubyforge.org
    #
    # The following options are supported:
    # 
    # * <tt>contentkey</tt>: The key to use for the content of text elements,
    #   defaults to '\_\_content__'
    # * <tt>forcearray</tt>: The list of elements which should always be returned
    #   as arrays.  Under normal circumstances single element arrays are inlined.
    # * <tt>suppressempty</tt>: The value to return for empty elements, pass +true+
    #   to remove empty elements entirely.
    # * <tt>keeproot</tt>:  By default the hash returned has a single key with the
    #   name of the root element.  If the name of the root element isn't 
    #   interesting to you, pass +false+. 
    # * <tt>forcecontent</tt>:  By default a text element with no attributes, will 
    #   be collapsed to just a string instead of a hash with a single key.  
    #   Pass +true+ to prevent this.
    #
    #
    def xml_in(string, options={})
      new(string, options).out
    end
  end
  
  def initialize(string, options) #:nodoc:
    @doc     = parse(string)
    @options = default_options.merge options
  end
  
  def out #:nodoc:
    if @options['keeproot']
      {@doc.root.name => collapse(@doc.root)}
    else
      collapse(@doc.root)
    end
  end
  
  private
    def default_options
      {'contentkey' => '__content__', 'forcearray' => [], 'keeproot'=>true}
    end
  
    def collapse(element)
      result = hash_of_attributes(element) 
      if text_node? element
        text = collapse_text(element)
        result[content_key] = text if text =~ /\S/
      elsif element.children?
        element.inject(result) do |hash, child|
          unless child.text?
            child_result = collapse(child) 
            (hash[child.name] ||= []) << child_result
          end
          hash
        end
      end
      if result.empty?
        return empty_element
      end
      # Compact them to ensure it complies with the user's requests
      inline_single_element_arrays(result) 
      remove_empty_elements(result) if suppress_empty?
      if content_only?(result) && !force_content?
        result[content_key]
      else
        result
      end
    end
    
    def content_only?(result)
      result.keys == [content_key]
    end
  
    def content_key
      @options['contentkey']
    end
  
    def force_array?(key_name)
      Array(@options['forcearray']).include?(key_name)
    end
  
    def inline_single_element_arrays(result)
      result.each do |key, value|
        if value.size == 1 && value.is_a?(Array) && !force_array?(key)
          result[key] = value.first
        end
      end    
    end
  
    def remove_empty_elements(result)
      result.each do |key, value|
        if value == empty_element
          result.delete key
        end
      end
    end
    
    def suppress_empty?
      @options['suppressempty'] == true
    end
     
    def empty_element
      if !@options.has_key? 'suppressempty'
        {}
      else
        @options['suppressempty']
      end
    end

    # removes the content if it's nothing but blanks,  prevents
    # the hash being polluted with lots of content like "\n\t\t\t" 
    def suppress_empty_content(result)
      result.delete content_key if result[content_key] !~ /\S/ 
    end
  
    def force_content?
      @options['forcecontent']
    end
  
    # a text node is one with 1 or more child nodes which are
    # text nodes, and no non-text children, there's no sensible
    # way to support nodes which are text and markup like:
    # <p>Something <b>Bold</b> </p>
    def text_node?(element)
      !element.text? && element.all? {|c| c.text?}
    end
  
    # takes a text node, and collapses it into a string
    def collapse_text(element)
      element.map {|c| c.content } * ''
    end
  
    def hash_of_attributes(element)
      result = {}
      element.each_attr do |attribute| 
        name = attribute.name
        name = [attribute.ns, attribute.name].join(':') if attribute.ns?
        result[name] = attribute.value 
      end
      result
    end
  
    def parse(string)
      if string == ''
        string = ' '
      end
      XML::Parser.string(string).parse
    end
end

class XmlSimple # :nodoc:
  def self.xml_in(*args)
    FasterXmlSimple.xml_in *args
  end
end
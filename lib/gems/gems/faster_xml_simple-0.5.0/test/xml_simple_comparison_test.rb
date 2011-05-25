require File.dirname(__FILE__) + '/test_helper'
require 'yaml'

class XmlSimpleComparisonTest < FasterXSTest
  
  # Define test methods
  
  Dir["test/fixtures/test-*.xml"].each do |file_name|
    xml_file_name = file_name
    method_name = File.basename(file_name, ".xml").gsub('-', '_')
    yml_file_name = file_name.gsub('xml', 'yml')
    rails_yml_file_name = file_name.gsub('xml', 'rails.yml')
    class_eval <<-EOV, __FILE__, __LINE__
      def #{method_name}
        assert_equal YAML.load(File.read('#{yml_file_name}')), 
              FasterXmlSimple.xml_in(File.read('#{xml_file_name}'), default_options )
      end
      
      def #{method_name}_rails
        assert_equal YAML.load(File.read('#{rails_yml_file_name}')), 
              FasterXmlSimple.xml_in(File.read('#{xml_file_name}'), rails_options)
      end
    EOV
  end
  
  def default_options
    {
       'keeproot'      => true, 
       'contentkey'    => '__content__', 
       'forcecontent'  => true, 
       'suppressempty' => nil,
       'forcearray'    => ['something-else']
    }
  end
  
  def rails_options
    {
      'forcearray'   => false,
      'forcecontent' => true,
      'keeproot'     => true,
      'contentkey'   => '__content__'
    }
  end
  

end
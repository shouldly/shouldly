require 'tempfile'
require 'nokogiri'
require 'httpclient'
require 'stringio'
require 'json'
require 'faster_xml_simple'

module Net
  module GitHub
    class Upload
      VERSION = '0.0.5'
      def initialize params=nil
        @login = params[:login]
        @token = params[:token]

        if @login.empty? or @token.empty?
          raise "login or token is empty"
        end
      end

      def upload info
        unless info[:repos]
          raise "required repository name"
        end
        info[:repos] = @login + '/' + info[:repos] unless info[:repos].include? '/'

        if info[:file]
          file = info[:file]
          unless File.exist?(file) && File.readable?(file)
            raise "file does not exsits or readable"
          end
          info[:name] ||= File.basename(file)
        end
        unless  info[:file] || info[:data]
          raise "required file or data parameter to upload"
        end

        unless info[:name]
          raise "required name parameter for filename with data parameter"
        end

        if info[:replace]
          list_files(info[:repos]).each { |obj|
            next unless obj[:name] == info[:name]
            delete info[:repos], obj[:id]
          }
        elsif list_files(info[:repos]).any?{|obj| obj[:name] == info[:name]}
          raise "file '#{info[:name]}' is already uploaded. please try different name"
        end

        info[:content_type] ||= 'application/octet-stream'
        stat = HTTPClient.post("https://github.com/#{info[:repos]}/downloads", {
          "file_size"    => info[:file] ? File.stat(info[:file]).size : info[:data].size,
          "content_type" => info[:content_type],
          "file_name"    => info[:name],
          "description"  => info[:description] || '',
          "login"        => @login,
          "token"        => @token
        })

        unless stat.code == 200
          raise "Failed to post file info"
        end

        upload_info = JSON.parse(stat.content)
        if info[:file]
          f = File.open(info[:file], 'rb')
        else
          f = Tempfile.open('net-github-upload')
          f << info[:data]
          f.flush
        end
        stat = HTTPClient.post("http://github.s3.amazonaws.com/", [
          ['Filename', info[:name]],
          ['policy', upload_info['policy']],
          ['success_action_status', 201],
          ['key', upload_info['path']],
          ['AWSAccessKeyId', upload_info['accesskeyid']],
          ['Content-Type', upload_info['content_type'] || 'application/octet-stream'],
          ['signature', upload_info['signature']],
          ['acl', upload_info['acl']],
          ['file', f]
        ])
        f.close

        if stat.code == 201
          return FasterXmlSimple.xml_in(stat.content)['PostResponse']['Location']
        else
          raise 'Failed to upload' + extract_error_message(stat)
        end
      end

      def replace info
         upload info.merge( :replace => true )
      end

      def delete_all repos
        unless repos
          raise "required repository name"
        end
        repos = @login + '/' + repos unless repos.include? '/'
        list_files(repos).each { |obj|
          delete repos, obj[:id]
        }
      end

      def extract_error_message(stat)
        # @see http://docs.amazonwebservices.com/AmazonS3/2006-03-01/ErrorResponses.html
        error = FasterXmlSimple.xml_in(stat.content)['Error']
        " due to #{error['Code']} (#{error['Message']})"
      rescue
        ''
      end

      def delete repos, id
        HTTPClient.post("https://github.com/#{repos}/downloads/#{id.gsub( "download_", '')}", {
          "_method"      => "delete",
          "login"        => @login,
          "token"        => @token
        })
      end

      def list_files repos
        raise "required repository name" unless repos
        res = HTTPClient.get_content("https://github.com/#{repos}/downloads", {
          "login" => @login,
          "token" => @token
        })
        Nokogiri::HTML(res).xpath('id("manual_downloads")/li').map do |fileinfo|
          obj = {
            :description => fileinfo.at_xpath('descendant::h4').text,
            :date        => fileinfo.at_xpath('descendant::p/abbr').attribute('title').text,
            :size        => fileinfo.at_xpath('descendant::p/strong').text,
            :id          => /\d+$/.match(fileinfo.at_xpath('a').attribute('href').text)[0]
          }
          anchor = fileinfo.at_xpath('descendant::h4/a')
          obj[:link] = anchor.attribute('href').text
          obj[:name] = anchor.text
          obj
        end
      end
    end
  end
end


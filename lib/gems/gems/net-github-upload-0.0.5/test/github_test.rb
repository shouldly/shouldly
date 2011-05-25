require 'test/unit'
require 'net/github-upload'

class GitHubUploadTest < Test::Unit::TestCase
	def setup
		login = `git config github.user`.chomp
		token = `git config github.token`.chomp
		@gh = Net::GitHub::Upload.new(
			:login => login,
			:token => token
		)
		@repos = 'ruby-net-github-upload'
    @gh.delete_all @repos
  end

	def test_file_upload
		direct_link = nil
		assert_nothing_raised {
			direct_link = @gh.upload(
				:repos => @repos,
				:file  => 'test/test',
				:content_type => 'text/plain',
				:description => "test file"
			)
		}
		assert_instance_of String, direct_link

		# replace method : thx id:rngtng
		assert_nothing_raised {
			direct_link = @gh.replace(
				:repos => @repos,
				:file  => 'test/test',
				:content_type => 'text/plain',
				:description => "test file"
			)
		}
		assert_instance_of String, direct_link
	end

	def test_content_upload
		direct_link = nil
		time = Time.now.to_i
		assert_nothing_raised {
			direct_link = @gh.upload(
				:repos => @repos,
				:data  => 'test',
				:name  => "test_#{time}.txt",
				:content_type => 'text/plain',
				:description => "test file2"
			)
		}
		assert_instance_of String, direct_link

		# replace method : thx id:rngtng
		assert_nothing_raised {
			direct_link = @gh.replace(
				:repos => @repos,
				:data  => 'test',
				:name  => "test_#{time}.txt",
				:content_type => 'text/plain',
				:description => "test file2"
			)
		}
		assert_instance_of String, direct_link
	end
end

prerequisite_gems = ['albacore']

installed_gems = `gem list`

prerequisite_gems.each do |gem|
  is_installed = installed_gems.include?(gem)
  if !is_installed
	puts "Installing pre-requisite gem #{gem}"
    `gem install #{gem}`
  end
  
  `gem install rubyzip -v 0.9.9`
  require gem
end
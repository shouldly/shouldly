prerequisite_gems = ['albacore']
prerequisite_gems = ['zip-zip']

installed_gems = `gem list`

prerequisite_gems.each do |gem|
  is_installed = installed_gems.include?(gem)
  if !is_installed
	puts "Installing pre-requisite gem #{gem}"
    `gem install #{gem}`
  end
  
  require gem
end
user_name = File.basename(`whoami`.chomp)
user_profile = "settings/#{user_name}.settings"	

unless File.exist?(user_profile)
  FileUtils.cp 'settings/settings_template', user_profile
  p "You may need to change the settings in the file #{user_profile}"
  exit
end

load user_profile

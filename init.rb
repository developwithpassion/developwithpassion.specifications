$LOAD_PATH.unshift("automation")

require 'bundler'
Bundler.setup

require_relative 'automation/setting_utils'
require 'expansions'
require_relative 'load_settings'
require 'thor'
require 'automation'
require 'ostruct'

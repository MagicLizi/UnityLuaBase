--[[
-- string扩展工具类，对string不支持的功能执行扩展
--]]

local unpack = unpack or table.unpack

-- 字符串分割
-- @split_string：被分割的字符串
-- @pattern：分隔符，可以为模式匹配
-- @init：起始位置
-- @plain：为true禁用pattern模式匹配；为false则开启模式匹配
local function split(split_string, pattern, search_pos_begin, plain)
	assert(type(split_string) == "string")
	assert(type(pattern) == "string" and #pattern > 0)
	search_pos_begin = search_pos_begin or 1
	plain = plain or true
	local split_result = {}

	while true do
		local find_pos_begin, find_pos_end = string.find(split_string, pattern, search_pos_begin, plain)
		if not find_pos_begin then
			split_result[#split_result + 1] = string.sub(split_string, search_pos_begin, string.len(split_string))
			break
		end
		local cur_str = ""
		if find_pos_begin > search_pos_begin then
			cur_str = string.sub(split_string, search_pos_begin, find_pos_begin - 1)
		end
		split_result[#split_result + 1] = cur_str
		search_pos_begin = find_pos_end + 1
	end

	--if search_pos_begin < string.len(split_string) then
	--split_result[#split_result + 1] = string.sub(split_string, search_pos_begin)
	--else
	--split_result[#split_result + 1] = ""
	--end

	return split_result
end

-- 字符串连接
function join(join_table, joiner)
	if #join_table == 0 then
		return ""
	end

	local fmt = "%s"
	for i = 2, #join_table do
		fmt = fmt .. joiner .. "%s"
	end

	return string.format(fmt, unpack(join_table))
end

-- 是否包含
-- 注意：plain为true时，关闭模式匹配机制，此时函数仅做直接的 “查找子串”的操作
function contains(target_string, pattern, plain)
	plain = plain or true
	local find_pos_begin, find_pos_end = string.find(target_string, pattern, 1, plain)
	return find_pos_begin ~= nil
end

-- 以某个字符串开始
function startswith(target_string, start_pattern, plain)
	plain = plain or true
	local find_pos_begin, find_pos_end = string.find(target_string, start_pattern, 1, plain)
	return find_pos_begin == 1
end

-- 以某个字符串结尾
function endswith(target_string, start_pattern, plain)
	plain = plain or true
	local find_pos_begin, find_pos_end = string.find(target_string, start_pattern, -#start_pattern, plain)
	return find_pos_end == #target_string
end

function findlast(target_string, start_pattern)
	local i=target_string:match(".*"..start_pattern.."()")
	if i==nil then return nil else return i-1 end
end

function IsNullOrEmpty(target_string)
	return target_string == nil or target_string == ""
end

local function charsize(ch)
	if not ch then return 0
	elseif ch >=252 then return 6
	elseif ch >= 248 and ch < 252 then return 5
	elseif ch >= 240 and ch < 248 then return 4
	elseif ch >= 224 and ch < 240 then return 3
	elseif ch >= 192 and ch < 224 then return 2
	elseif ch < 192 then return 1
	end
end

function utf8len(str)
	local len = 0
	local aNum = 0 --字母个数
	local hNum = 0 --汉字个数
	local currentIndex = 1
	while currentIndex <= #str do
		local char = string.byte(str, currentIndex)
		local cs = charsize(char)
		currentIndex = currentIndex + cs
		len = len +1
		if cs == 1 then
			aNum = aNum + 1
		elseif cs >= 2 then
			hNum = hNum + 1
		end
	end
	return len, aNum, hNum
end

function utf8sub(str, startChar, numChars)
	local startIndex = 1
	while startChar > 1 do
		local char = string.byte(str, startIndex)
		startIndex = startIndex + charsize(char)
		startChar = startChar - 1
	end

	local currentIndex = startIndex
	while numChars > 0 and currentIndex <= #str do
		local char = string.byte(str, currentIndex)
		currentIndex = currentIndex + charsize(char)
		numChars = numChars -1
	end
	return str:sub(startIndex, currentIndex - 1)
end

function replace(s, pattern, repl)
	local i,j = string.find(s, pattern, 1, true)
	if i and j then
		local ret = {}
		local start = 1
		while i and j do
			table.insert(ret, string.sub(s, start, i - 1))
			table.insert(ret, repl)
			start = j + 1
			i,j = string.find(s, pattern, start, true)
		end
		table.insert(ret, string.sub(s, start))
		return table.concat(ret)
	end
	return s
end

function ljust(str, n, ch)
	if str == nil then
		return nil, "the string parameter is nil"
	end
	ch = ch or " "
	n = tonumber(n) or 0
	local len = string.len(str)
	return string.rep(ch, n - len) .. str
end

function rjust(str, n, ch)
	if str == nil then
		return nil, "the string parameter is nil"
	end
	ch = ch or " "
	n = tonumber(n) or 0
	local len = string.len(str)
	return str .. string.rep(ch, n - len)
end

-- 时间转字符串
local function timeToString(t, joiner)
	joiner = joiner or ":"
	local timeJust = function(n)
		return string.ljust(tostring(n), 2, "0")
	end
	local times = {timeJust(math.floor(t/3600)),timeJust(math.floor(t/60)%60),timeJust(t%60)}
	return string.join(times, joiner)
end

string.split = split
string.join = join
string.contains = contains
string.startswith = startswith
string.endswith = endswith
string.findlast = findlast
string.IsNullOrEmpty = IsNullOrEmpty
string.utf8len = utf8len
string.utf8sub = utf8sub
string.replace =replace
string.ljust = ljust
string.rjust = rjust
string.timeToString = timeToString


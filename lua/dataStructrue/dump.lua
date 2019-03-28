---
--格式化输出table（格式化过程中，排序操作会比较耗时）
--@module dump
--@author myc

 
-- local View = import("bos.engine.ui.widget").View;
local ignoreEngineObj = true;

local isSort  = false;


local tostring = tostring
local type = type
local pairs = pairs
local ipairs = ipairs
local table_format = string.format
local string_len = string.len
local string_rep = string.rep

local math_randomseed = math.randomseed
local debug_traceback = debug.traceback

function string.split(str, delimiter)
    if (delimiter == '') then return false end
    local pos, arr = 0, {}
    -- for each divider found
    for st, sp in function() return string.find(str, delimiter, pos, true) end do
        table.insert(arr, string.sub(str, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(str, pos))
    return arr
end

function string.trim(str, char)
    if (char == '' or char == nil) then
        char = '%s'
    end

    local trimmed = string.gsub(str, '^' .. char .. '*(.-)' .. char .. '*$', '%1')
    return trimmed
end



local function _dump_value(v)
    if type(v) == "string" then
        v = string.format("%q", v)
    end
    if type(v) == "function" or type(v) == "userdata" then
        v = string.format("%q", tostring(v))
    end
    return tostring(v)
end

 
function dump(value, desciption, nesting)

    -- if not BYKit.BYKitConfig or not BYKit.BYKitConfig.isDebug then
    --     return;
    -- end
 
    if type(nesting) ~= "number" then nesting = 10 end

    local lookup = {}
    local result = {}
    local traceback = string.split(debug_traceback("", 2), "\n")
    
    -- sys_set_int("win32_console_color", 10)
    --print("dump from: " .. string.trim(traceback[3]))
    local str = "- dump from: " .. string.trim(traceback[3]);
    print(str);
    local function _dump(value, desciption, indent, nest, keylen)
        desciption = desciption or "<var>"
        local spc = ""
        if type(keylen) == "number" then
            spc = string_rep(" ", keylen - string_len(_dump_value(desciption)))
        end
        if type(value) ~= "table" then
            result[#result +1 ] = table_format("%s%s%s = %s", indent, _dump_value(desciption), spc, _dump_value(value))
        elseif lookup[tostring(value)] then
            result[#result +1 ] = table_format("%s%s%s = *REF*", indent, _dump_value(desciption), spc)
        -- elseif typeof and DrawingBase and typeof(value,DrawingBase) and ignoreEngineObj then
        --     result[#result +1 ] = table_format("%s%s%s = *DrawingBase_REF*", indent, _dump_value(desciption), spc)
        elseif value._layer and value.update_layout and ignoreEngineObj then
            result[#result +1 ] = table_format("%s%s%s = 'UI.VIEW_REF_'", indent, _dump_value(desciption), spc)
        else
            lookup[tostring(value)] = true
            if nest > nesting then
                result[#result +1 ] = table_format("%s%s = *MAX NESTING*", indent, _dump_value(desciption))
            else
                result[#result +1 ] = table_format("%s%s = {", indent, _dump_value(desciption))
                local indent2 = indent.."    "
                local keys = {}
                local keylen = 0
                local values = {}
                for k, v in pairs(value) do
                    if k~="___message" then
                        keys[#keys + 1] = k
                        local vk = _dump_value(k)
                        local vkl = string_len(vk)
                        if vkl > keylen then keylen = vkl end
                        values[k] = v
                    end
                end
                if isSort == true then
                    table.sort(keys, function(a, b)
                        if type(a) == "number" and type(b) == "number" then
                            return a < b
                        else
                            return tostring(a) < tostring(b)
                        end
                    end)
                end


                for i, k in ipairs(keys) do
                    _dump(values[k], k, indent2, nest + 1, keylen)
                end
                result[#result +1] = table_format("%s}", indent)
            end
        end
    end
    _dump(value, desciption, "- ", 1)

    
    local t = table.concat(result,"\n");
     print(t)
 end
 
-- dump({[1] = "222", asdfae = {"dds"}})
 

return dump;
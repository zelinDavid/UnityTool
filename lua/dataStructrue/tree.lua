-- author : coder_zhang
-- date   : 2014-6-25
 
require("dump")

--将table转为tree
function  insertToTreeFromArr(arr,parent)
    if not parent then 
        local root = {}
        local children = arr.children 
        arr.children = nil
        root.value = arr 
       
        if children and #children > 0 then 
            root.children = {}
            for i,child in ipairs(children) do
                local node =  insertToTreeFromArr(child, root)
                table.insert(root.children, node)
            end
        end
        return root
    else
        local children = arr.children 
        arr.children = nil
        local node = {}
        node.parent = parent 
        node.value = arr 
        if children and #children > 0 then 
            node.children = {}
            for i,child in ipairs(children) do
              local childNode =  insertToTreeFromArr(child, node)
              table.insert(node.children, childNode)
            end
        end 
        return node 
     end
end
 
 
local tab = {
    [1] = {
        ["flex_direction"] = 0,
        ["left"] = 426,
        ["width"] = 200,
        ["show"] = false,
        ["viewType"] = "View",
        ["height"] = 200,
        ["top"] = 240,
        ["children"] = {
            [1] = {
                ["flex_direction"] = 0,
                ["left"] = 91,
                ["width"] = 100,
                ["show"] = false,
                ["viewType"] = "Button",
                ["height"] = 50,
                ["top"] = 86,
                ["position_type"] = 1
            },
            [2] = {
                ["flex_direction"] = 0,
                ["left"] = 63,
                ["width"] = 100,
                ["show"] = false,
                ["viewType"] = "Button",
                ["height"] = 50,
                ["top"] = 44,
                ["position_type"] = 1
            }
        },
        ["position_type"] = 1
    },
    [2] = {
        ["flex_direction"] = 0,
        ["left"] = 467,
        ["width"] = "auto",
        ["show"] = false,
        ["viewType"] = "Label",
        ["height"] = 40,
        ["top"] = 200,
        ["position_type"] = 1
    },
    [3] = {
        ["flex_direction"] = 0,
        ["left"] = 794,
        ["width"] = "auto",
        ["show"] = false,
        ["viewType"] = "Checkbox",
        ["height"] = "auto",
        ["top"] = 329,
        ["position_type"] = 1
    }
}

local temRoot = {
    ["flex_direction"] = 0,
    ["left"] = 426,
    ["width"] = 200,
    ["show"] = false,
    ["viewType"] = "View",
    ["height"] = 200,
    ["top"] = 240,
    children = tab,
  
}

 
local tree = insertToTreeFromArr(temRoot, nil )

local json =  require("json")

local ret = json.encode(tree)
print(ret)
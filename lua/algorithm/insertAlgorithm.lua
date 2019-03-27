local sort = {

}

local dump = function (  tab )
    if type(tab) == "table" then 
        print(table.concat(tab," "))
    end 
end

function sort.insertSort( list, compareFunc )
    for i = 1, #list do
        local tem = list[i]
        local j = i
        while j > 1 and compareFunc(tem, list[j - 1]) do
            list[j] = list[j - 1]
            j = j - 1
        end
        list[j] = tem
    end
end

--谢尔排序 （分组插入排序）
--@param list 数据
--@param compareFunc 比较函数
function sort.shellSort( list, compareFunc )
    local grop = math.floor(#list / 2)
    while grop > 0 do
        for i = grop + 1, #list do
            local tem = list[i]
            local j = i
            while j > grop and compareFunc(tem, list[j - grop]) do
                list[j] = list[j - grop]
                j = j - grop
            end
            list[j] = tem
        end
        grop = math.floor(grop / 2)
    end
 
end


local dd = {1,4,25,764, 34, 645, 67, 6354, 34,}
local tem = {}
for i=1,10 do
    for i,v in ipairs(dd) do
        table.insert(tem,v)
    end
end

-- print("------------------------------insert sort----------------------------------")
-- for i=1,100000 do
--     local list = tem
--     sort.insertSort(list, function ( a, b )
--         return a < b
--     end)
-- end


print("------------------------------shell sort----------------------------------")
for i=1,100000 do
    local list = tem
    sort.shellSort(list, function ( a, b )
        return a < b
    end)
end

 

 
 

 
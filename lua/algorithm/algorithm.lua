
--分治算法求最大子数组
--将数组从中间拆分为n个小数组，每个小数组中比较三种情况中最大的数组，并向上返回进行递归比较，最终获取到最大的子数组

local tab = {23, -99, 18, -4, 2, 1, 23, 43, 9, -9, 6, 888, 23, -99, 18}

-- local tab = { -4, 2, 1, 23}
 divid = function(start, endIndex, arr)
    if start == endIndex then
        local tab = {
            start = start,
            endIndex = endIndex,
            count = arr[start]
        }
        return tab
    end

    local mid = math.ceil((start + endIndex) /2)

    print(start, endIndex, mid )

    local lowArr = divid(start, mid - 1, arr)
    local highArr = divid(mid , endIndex, arr)

    local arr3 = {}
    local leftPart = {}
    local count = 0
    local leftMax = 0
    local leftStart
    for i = mid, 1, -1 do
        count = count + arr[i]
        if count > leftMax then
            leftMax = count
            leftStart = i
        end
    end
    arr3.start = leftStart

    leftPart = {}
    count = 0
    local rightMax = 0
    local right = mid + 1
    for i = mid + 1, endIndex do
        count = count + arr[i]
        if count > rightMax then
            rightMax = count
            right = i
        end
    end

    arr3.endIndex = right 
    arr3.count = leftMax + rightMax

    local max = lowArr.count > highArr.count and lowArr or highArr
    max = max.count > arr3.count and max or arr3
    return max 
end

 
local max = divid(1, #tab, tab)

 

 
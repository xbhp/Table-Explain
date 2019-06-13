var getLocalization = function (culture) {
    var localization = null;
    switch (culture) {        
        case "cn":
            localization =
            {
                // separator of parts of a date (e.g. '/' in 11/05/1955)
                '/': "/",
                // separator of parts of a time (e.g. ':' in 05:44 PM)
                ':': "：",
                // the first day of the week (0 = Sunday, 1 = Monday, etc)
                firstDay: 0,
                days: {
                    // full day names
                    names: ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
                    // abbreviated day names
                    namesAbbr: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
                    // shortest day names
                    namesShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"]
                },
                months: {
                    // full month names (13 months for lunar calendards -- 13th month should be "" if not lunar)
                    names: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月", ""],
                    // abbreviated month names
                    namesAbbr: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月", ""]
                },
                // AM and PM designators in one of these forms:
                // The usual view, and the upper and lower case versions
                //      [standard,lowercase,uppercase]
                // The culture does not use AM or PM (likely all standard date formats use 24 hour time)
                //      null
                AM: ["AM", "am", "上午"],
                PM: ["PM", "pm", "下午"],
                eras: [
                // eras in reverse chronological order.
                // name: the name of the era in this culture (e.g. A.D., C.E.)
                // start: when the era starts in ticks (gregorian, gmt), null if it is the earliest supported era.
                // offset: offset in years from gregorian calendar
                { "name": "A.D.", "start": null, "offset": 0 }
                ],
                twoDigitYearMax: 2029,
                patterns: {
                    // short date pattern
                    d: "M/d/yyyy",
                    // long date pattern
                    D: "dddd, MMMM dd, yyyy",
                    // short time pattern
                    t: "h:mm tt",
                    // long time pattern
                    T: "h:mm:ss tt",
                    // long date, short time pattern
                    f: "dddd, MMMM dd, yyyy h:mm tt",
                    // long date, long time pattern
                    F: "dddd, MMMM dd, yyyy h:mm:ss tt",
                    // month/day pattern
                    M: "MMMM dd",
                    // month/year pattern
                    Y: "yyyy MMMM",
                    // S is a sortable format that does not vary by culture
                    S: "yyyy\u0027-\u0027MM\u0027-\u0027dd\u0027T\u0027HH\u0027:\u0027mm\u0027:\u0027ss",
                    // formatting of dates in MySQL DataBases
                    ISO: "yyyy-MM-dd hh:mm:ss",
                    ISO2: "yyyy-MM-dd HH:mm:ss",
                    ISO3: "yyyy年MM月dd日 HH时mm分ss秒",
                    d1: "dd.MM.yyyy",
                    d2: "dd-MM-yyyy",
                    d3: "dd-MMMM-yyyy",
                    d4: "dd-MM-yy",
                    d5: "H:mm",
                    d6: "HH:mm",
                    d7: "HH:mm tt",
                    d8: "dd/MMMM/yyyy",
                    d9: "MMMM-dd",
                    d10: "MM-dd",
                    d11: "MM-dd-yyyy",
                    d12: "yyyy年MM月dd日",
                    d13: "HH时mm分ss秒"
                },
                percentsymbol: "%",
                currencysymbol: "￥",
                currencysymbolposition: "before",
                decimalseparator: '.',
                thousandsseparator: ',',
                pagergotopagestring: "跳转到:",
                pagershowrowsstring: "显示行数:",
                pagerrangestring: " - ",
                pagerpreviousbuttonstring: "上一页",
                pagernextbuttonstring: "下一页",
                pagerfirstbuttonstring: "首页",
                pagerlastbuttonstring: "尾页",
                groupsheaderstring: "将要分组的字段拖拽至此",
                sortascendingstring: "升序排列",
                sortdescendingstring: "降序排列",
                sortremovestring: "取消排序",
                groupbystring: "按此分组",
                groupremovestring: "移出组合",
                filterclearstring: "清除",
                filterstring: "过滤",
                filtershowrowstring: "显示行：",
                filterorconditionstring: "或",
                filterandconditionstring: "和",
                filterselectallstring: "(全选)",
                filterchoosestring: "请选择:",
                filterstringcomparisonoperators: ['为空', '不为空', '辅助', '辅助(区分大小写)',
                   '不包含', '不包含(区分大小写)', '开头', '开头(区分大小写)',
                   '结尾', '结尾(区分大小写)', '等于', '等于(区分大小写)', '为空', '不为空'],
                filternumericcomparisonoperators: ['=', '<>', '<', '<=', '>', '>=', '空', '非空'],
                filterdatecomparisonoperators: ['等于', '不等于', '小于', '小于等于', '大于', '大于等于', '为空', '不为空'],
                filterbooleancomparisonoperators: ['等于', '不等于'],
                validationstring: "输入信息无效",
                emptydatastring: "没有可显示的数据",
                filterselectstring: "选择过滤器",
                loadtext: "加载中...",
                clearstring: "清除",
                todaystring: "今天"
             }
            break;
         
    }
    return localization;
}
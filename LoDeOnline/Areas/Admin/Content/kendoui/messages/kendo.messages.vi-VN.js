/** 
 * Kendo UI v2017.3.1026 (http://www.telerik.com/kendo-ui)                                                                                                                                              
 * Copyright 2017 Telerik AD. All rights reserved.                                                                                                                                                      
 *                                                                                                                                                                                                      
 * Kendo UI commercial licenses may be obtained at                                                                                                                                                      
 * http://www.telerik.com/purchase/license-agreement/kendo-ui-complete                                                                                                                                  
 * If you do not own a commercial license, this file shall be governed by the trial license terms.                                                                                                      
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       
                                                                                                                                                                                                       

*/
! function(e) {
    "function" == typeof define && define.amd ? define(["kendo.core.min"], e) : e()
}(function() {
    ! function(e, t) {
        kendo.ui.FlatColorPicker && (kendo.ui.FlatColorPicker.prototype.options.messages = e.extend(!0, kendo.ui.FlatColorPicker.prototype.options.messages, {
            apply: "Apply",
            cancel: "Cancel",
            noColor: "no color",
            clearColor: "Clear color"
        })), kendo.ui.ColorPicker && (kendo.ui.ColorPicker.prototype.options.messages = e.extend(!0, kendo.ui.ColorPicker.prototype.options.messages, {
            apply: "Apply",
            cancel: "Cancel",
            noColor: "no color",
            clearColor: "Clear color"
        })), kendo.ui.ColumnMenu && (kendo.ui.ColumnMenu.prototype.options.messages = e.extend(!0, kendo.ui.ColumnMenu.prototype.options.messages, {
            sortAscending: "Sort Ascending",
            sortDescending: "Sort Descending",
            filter: "Filter",
            columns: "Columns",
            done: "Done",
            settings: "Column Settings",
            lock: "Lock",
            unlock: "Unlock"
        })), kendo.ui.Editor && (kendo.ui.Editor.prototype.options.messages = e.extend(!0, kendo.ui.Editor.prototype.options.messages, {
            bold: "Bold",
            italic: "Italic",
            underline: "Underline",
            strikethrough: "Strikethrough",
            superscript: "Superscript",
            subscript: "Subscript",
            justifyCenter: "Center text",
            justifyLeft: "Align text left",
            justifyRight: "Align text right",
            justifyFull: "Justify",
            insertUnorderedList: "Insert unordered list",
            insertOrderedList: "Insert ordered list",
            indent: "Indent",
            outdent: "Outdent",
            createLink: "Insert hyperlink",
            unlink: "Remove hyperlink",
            insertImage: "Insert image",
            insertFile: "Insert file",
            insertHtml: "Insert HTML",
            viewHtml: "View HTML",
            fontName: "Select font family",
            fontNameInherit: "(inherited font)",
            fontSize: "Select font size",
            fontSizeInherit: "(inherited size)",
            formatBlock: "Format",
            formatting: "Format",
            foreColor: "Color",
            backColor: "Background color",
            style: "Styles",
            emptyFolder: "Empty Folder",
            uploadFile: "Upload",
            overflowAnchor: "More tools",
            orderBy: "Arrange by:",
            orderBySize: "Size",
            orderByName: "Name",
            invalidFileType: 'The selected file "{0}" is not valid. Supported file types are {1}.',
            deleteFile: 'Are you sure you want to delete "{0}"?',
            overwriteFile: 'A file with name "{0}" already exists in the current directory. Do you want to overwrite it?',
            directoryNotFound: "A directory with this name was not found.",
            imageWebAddress: "Web address",
            imageAltText: "Alternate text",
            imageWidth: "Width (px)",
            imageHeight: "Height (px)",
            fileWebAddress: "Web address",
            fileTitle: "Title",
            linkWebAddress: "Web address",
            linkText: "Text",
            linkToolTip: "ToolTip",
            linkOpenInNewWindow: "Open link in new window",
            dialogUpdate: "Update",
            dialogInsert: "Insert",
            dialogButtonSeparator: "or",
            dialogCancel: "Cancel",
            cleanFormatting: "Clean formatting",
            createTable: "Create table",
            addColumnLeft: "Add column on the left",
            addColumnRight: "Add column on the right",
            addRowAbove: "Add row above",
            addRowBelow: "Add row below",
            deleteRow: "Delete row",
            deleteColumn: "Delete column",
            dialogOk: "Ok",
            tableWizard: "Table Wizard",
            tableTab: "Table",
            cellTab: "Cell",
            accessibilityTab: "Accessibility",
            caption: "Caption",
            summary: "Summary",
            width: "Width",
            height: "Height",
            units: "Units",
            cellSpacing: "Cell Spacing",
            cellPadding: "Cell Padding",
            cellMargin: "Cell Margin",
            alignment: "Alignment",
            background: "Background",
            cssClass: "CSS Class",
            id: "ID",
            border: "Border",
            borderStyle: "Border Style",
            collapseBorders: "Collapse borders",
            wrapText: "Wrap text",
            associateCellsWithHeaders: "Associate cells with headers",
            alignLeft: "Align Left",
            alignCenter: "Align Center",
            alignRight: "Align Right",
            alignLeftTop: "Align Left Top",
            alignCenterTop: "Align Center Top",
            alignRightTop: "Align Right Top",
            alignLeftMiddle: "Align Left Middle",
            alignCenterMiddle: "Align Center Middle",
            alignRightMiddle: "Align Right Middle",
            alignLeftBottom: "Align Left Bottom",
            alignCenterBottom: "Align Center Bottom",
            alignRightBottom: "Align Right Bottom",
            alignRemove: "Remove Alignment",
            columns: "Columns",
            rows: "Rows",
            selectAllCells: "Select All Cells"
        })), kendo.ui.FileBrowser && (kendo.ui.FileBrowser.prototype.options.messages = e.extend(!0, kendo.ui.FileBrowser.prototype.options.messages, {
            uploadFile: "Upload",
            orderBy: "Arrange by",
            orderByName: "Name",
            orderBySize: "Size",
            directoryNotFound: "A directory with this name was not found.",
            emptyFolder: "Empty Folder",
            deleteFile: 'Are you sure you want to delete "{0}"?',
            invalidFileType: 'The selected file "{0}" is not valid. Supported file types are {1}.',
            overwriteFile: 'A file with name "{0}" already exists in the current directory. Do you want to overwrite it?',
            dropFilesHere: "drop file here to upload",
            search: "Search"
        })), kendo.ui.FilterCell && (kendo.ui.FilterCell.prototype.options.messages = e.extend(!0, kendo.ui.FilterCell.prototype.options.messages, {
            isTrue: "is true",
            isFalse: "is false",
            filter: "Filter",
            clear: "Clear",
            operator: "Operator"
        })), kendo.ui.FilterCell && (kendo.ui.FilterCell.prototype.options.operators = e.extend(!0, kendo.ui.FilterCell.prototype.options.operators, {
            string: {
                eq: "Is equal to",
                neq: "Is not equal to",
                startswith: "Starts with",
                contains: "Contains",
                doesnotcontain: "Does not contain",
                endswith: "Ends with",
                isnull: "Is null",
                isnotnull: "Is not null",
                isempty: "Is empty",
                isnotempty: "Is not empty"
            },
            number: {
                eq: "Is equal to",
                neq: "Is not equal to",
                gte: "Is greater than or equal to",
                gt: "Is greater than",
                lte: "Is less than or equal to",
                lt: "Is less than",
                isnull: "Is null",
                isnotnull: "Is not null"
            },
            date: {
                eq: "Is equal to",
                neq: "Is not equal to",
                gte: "Is after or equal to",
                gt: "Is after",
                lte: "Is before or equal to",
                lt: "Is before",
                isnull: "Is null",
                isnotnull: "Is not null"
            },
            enums: {
                eq: "Is equal to",
                neq: "Is not equal to",
                isnull: "Is null",
                isnotnull: "Is not null"
            }
        })), kendo.ui.FilterMenu && (kendo.ui.FilterMenu.prototype.options.messages = e.extend(!0, kendo.ui.FilterMenu.prototype.options.messages, {
            info: "Hiện những dòng với giá trị:",
            isTrue: "is true",
            isFalse: "is false",
            filter: "Lọc",
            clear: "Clear",
            and: "Và",
            or: "Hoặc",
            selectValue: "-Chọn giá trị-",
            operator: "Toán tử",
            value: "Giá trị",
            cancel: "Hủy bỏ"
        })), kendo.ui.FilterMenu && (kendo.ui.FilterMenu.prototype.options.operators = e.extend(!0, kendo.ui.FilterMenu.prototype.options.operators, {
            string: {
                eq: "Bằng",
                neq: "Không bằng",
                startswith: "Bắt đầu với",
                contains: "Chứa",
                doesnotcontain: "Không chứa",
                endswith: "Kết thúc với",
                isnull: "Bằng null",
                isnotnull: "Không bằng null",
                isempty: "Is empty",
                isnotempty: "Is not empty"
            },
            number: {
                eq: "Bằng",
                neq: "Không bằng",
                gte: "Lớn hơn hoặc bằng",
                gt: "Lớn hơn",
                lte: "Nhỏ hơn hoặc bằng",
                lt: "Nhỏ hơn",
                isnull: "Is null",
                isnotnull: "Is not null"
            },
            date: {
                eq: "Bằng",
                neq: "Không bằng",
                gte: "Lớn hơn hoặc bằng",
                gt: "Lớn hơn",
                lte: "Nhỏ hơn hoặc bằng",
                lt: "Nhỏ hơn",
                isnull: "Is null",
                isnotnull: "Is not null"
            },
            enums: {
                eq: "Bằng",
                neq: "Không bằng",
                isnull: "Is null",
                isnotnull: "Is not null"
            }
        })), kendo.ui.FilterMultiCheck && (kendo.ui.FilterMultiCheck.prototype.options.messages = e.extend(!0, kendo.ui.FilterMultiCheck.prototype.options.messages, {
            checkAll: "Select All",
            clear: "Clear",
            filter: "Filter",
            search: "Search"
        })), kendo.ui.Gantt && (kendo.ui.Gantt.prototype.options.messages = e.extend(!0, kendo.ui.Gantt.prototype.options.messages, {
            actions: {
                addChild: "Add Child",
                append: "Add Task",
                insertAfter: "Add Below",
                insertBefore: "Add Above",
                pdf: "Export to PDF"
            },
            cancel: "Cancel",
            deleteDependencyWindowTitle: "Delete dependency",
            deleteTaskWindowTitle: "Delete task",
            destroy: "Delete",
            editor: {
                assingButton: "Assign",
                editorTitle: "Task",
                end: "End",
                percentComplete: "Complete",
                resources: "Resources",
                resourcesEditorTitle: "Resources",
                resourcesHeader: "Resources",
                start: "Start",
                title: "Title",
                unitsHeader: "Units"
            },
            save: "Save",
            views: {
                day: "Day",
                end: "End",
                month: "Month",
                start: "Start",
                week: "Week",
                year: "Year"
            }
        })), kendo.ui.Grid && (kendo.ui.Grid.prototype.options.messages = e.extend(!0, kendo.ui.Grid.prototype.options.messages, {
            commands: {
                cancel: "Cancel changes",
                canceledit: "Cancel",
                create: "Add new record",
                destroy: "Delete",
                edit: "Edit",
                excel: "Export to Excel",
                pdf: "Export to PDF",
                save: "Save changes",
                select: "Select",
                update: "Update"
            },
            editable: {
                cancelDelete: "Cancel",
                confirmation: "Are you sure you want to delete this record?",
                confirmDelete: "Delete"
            },
            noRecords: "No records available.",
            expandCollapseColumnHeader: ""
        })), kendo.ui.TreeList && (kendo.ui.TreeList.prototype.options.messages = e.extend(!0, kendo.ui.TreeList.prototype.options.messages, {
            noRows: "No records to display",
            loading: "Loading...",
            requestFailed: "Request failed.",
            retry: "Retry",
            commands: {
                edit: "Edit",
                update: "Update",
                canceledit: "Cancel",
                create: "Add new record",
                createchild: "Add child record",
                destroy: "Delete",
                excel: "Export to Excel",
                pdf: "Export to PDF"
            }
        })), kendo.ui.Groupable && (kendo.ui.Groupable.prototype.options.messages = e.extend(!0, kendo.ui.Groupable.prototype.options.messages, {
            empty: "Drag a column header and drop it here to group by that column"
        })), kendo.ui.NumericTextBox && (kendo.ui.NumericTextBox.prototype.options = e.extend(!0, kendo.ui.NumericTextBox.prototype.options, {
            upArrowText: "Increase value",
            downArrowText: "Decrease value"
        })), kendo.ui.MediaPlayer && (kendo.ui.MediaPlayer.prototype.options.messages = e.extend(!0, kendo.ui.MediaPlayer.prototype.options.messages, {
            pause: "Pause",
            play: "Play",
            mute: "Mute",
            unmute: "Unmute",
            quality: "Quality",
            fullscreen: "Full Screen"
        })), kendo.ui.Pager && (kendo.ui.Pager.prototype.options.messages = e.extend(!0, kendo.ui.Pager.prototype.options.messages, {
            allPages: "All",
            display: "{0} - {1} của {2} dòng",
            empty: "No items to display",
            page: "Page",
            of: "of {0}",
            itemsPerPage: "Số dòng trên trang",
            first: "Đi tới trang đầu",
            previous: "Đi tới trang trước",
            next: "Đi tới trang kế tiếp",
            last: "Đi tới trang cuối",
            refresh: "Refresh",
            morePages: "More pages"
        })), kendo.ui.PivotGrid && (kendo.ui.PivotGrid.prototype.options.messages = e.extend(!0, kendo.ui.PivotGrid.prototype.options.messages, {
            measureFields: "Drop Data Fields Here",
            columnFields: "Drop Column Fields Here",
            rowFields: "Drop Rows Fields Here"
        })), kendo.ui.PivotFieldMenu && (kendo.ui.PivotFieldMenu.prototype.options.messages = e.extend(!0, kendo.ui.PivotFieldMenu.prototype.options.messages, {
            info: "Show items with value that:",
            filterFields: "Fields Filter",
            filter: "Filter",
            include: "Include Fields...",
            title: "Fields to include",
            clear: "Clear",
            ok: "Ok",
            cancel: "Cancel",
            operators: {
                contains: "Contains",
                doesnotcontain: "Does not contain",
                startswith: "Starts with",
                endswith: "Ends with",
                eq: "Is equal to",
                neq: "Is not equal to"
            }
        })), kendo.ui.RecurrenceEditor && (kendo.ui.RecurrenceEditor.prototype.options.messages = e.extend(!0, kendo.ui.RecurrenceEditor.prototype.options.messages, {
            frequencies: {
                never: "Never",
                hourly: "Hourly",
                daily: "Daily",
                weekly: "Weekly",
                monthly: "Monthly",
                yearly: "Yearly"
            },
            hourly: {
                repeatEvery: "Repeat every: ",
                interval: " hour(s)"
            },
            daily: {
                repeatEvery: "Repeat every: ",
                interval: " day(s)"
            },
            weekly: {
                interval: " week(s)",
                repeatEvery: "Repeat every: ",
                repeatOn: "Repeat on: "
            },
            monthly: {
                repeatEvery: "Repeat every: ",
                repeatOn: "Repeat on: ",
                interval: " month(s)",
                day: "Day "
            },
            yearly: {
                repeatEvery: "Repeat every: ",
                repeatOn: "Repeat on: ",
                interval: " year(s)",
                of: " of "
            },
            end: {
                label: "End:",
                mobileLabel: "Ends",
                never: "Never",
                after: "After ",
                occurrence: " occurrence(s)",
                on: "On "
            },
            offsetPositions: {
                first: "first",
                second: "second",
                third: "third",
                fourth: "fourth",
                last: "last"
            },
            weekdays: {
                day: "day",
                weekday: "weekday",
                weekend: "weekend day"
            }
        })), kendo.ui.Scheduler && (kendo.ui.Scheduler.prototype.options.messages = e.extend(!0, kendo.ui.Scheduler.prototype.options.messages, {
            allDay: "all day",
            date: "Date",
            event: "Event",
            time: "Time",
            showFullDay: "Show full day",
            showWorkDay: "Show business hours",
            today: "Today",
            save: "Save",
            cancel: "Cancel",
            destroy: "Delete",
            deleteWindowTitle: "Delete event",
            ariaSlotLabel: "Selected from {0:t} to {1:t}",
            ariaEventLabel: "{0} on {1:D} at {2:t}",
            editable: {
                confirmation: "Are you sure you want to delete this event?"
            },
            views: {
                day: "Day",
                week: "Week",
                workWeek: "Work Week",
                agenda: "Agenda",
                month: "Month"
            },
            recurrenceMessages: {
                deleteWindowTitle: "Delete Recurring Item",
                deleteWindowOccurrence: "Delete current occurrence",
                deleteWindowSeries: "Delete the series",
                editWindowTitle: "Edit Recurring Item",
                editWindowOccurrence: "Edit current occurrence",
                editWindowSeries: "Edit the series",
                deleteRecurring: "Do you want to delete only this event occurrence or the whole series?",
                editRecurring: "Do you want to edit only this event occurrence or the whole series?"
            },
            editor: {
                title: "Title",
                start: "Start",
                end: "End",
                allDayEvent: "All day event",
                description: "Description",
                repeat: "Repeat",
                timezone: " ",
                startTimezone: "Start timezone",
                endTimezone: "End timezone",
                separateTimezones: "Use separate start and end time zones",
                timezoneEditorTitle: "Timezones",
                timezoneEditorButton: "Time zone",
                timezoneTitle: "Time zones",
                noTimezone: "No timezone",
                editorTitle: "Event"
            }
        })), kendo.spreadsheet && kendo.spreadsheet.messages.borderPalette && (kendo.spreadsheet.messages.borderPalette = e.extend(!0, kendo.spreadsheet.messages.borderPalette, {
            allBorders: "All borders",
            insideBorders: "Inside borders",
            insideHorizontalBorders: "Inside horizontal borders",
            insideVerticalBorders: "Inside vertical borders",
            outsideBorders: "Outside borders",
            leftBorder: "Left border",
            topBorder: "Top border",
            rightBorder: "Right border",
            bottomBorder: "Bottom border",
            noBorders: "No border",
            reset: "Reset color",
            customColor: "Custom color...",
            apply: "Apply",
            cancel: "Cancel"
        })), kendo.spreadsheet && kendo.spreadsheet.messages.dialogs && (kendo.spreadsheet.messages.dialogs = e.extend(!0, kendo.spreadsheet.messages.dialogs, {
            apply: "Apply",
            save: "Save",
            cancel: "Cancel",
            remove: "Remove",
            retry: "Retry",
            revert: "Revert",
            okText: "OK",
            formatCellsDialog: {
                title: "Format",
                categories: {
                    number: "Number",
                    currency: "Currency",
                    date: "Date"
                }
            },
            fontFamilyDialog: {
                title: "Font"
            },
            fontSizeDialog: {
                title: "Font size"
            },
            bordersDialog: {
                title: "Borders"
            },
            alignmentDialog: {
                title: "Alignment",
                buttons: {
                    justtifyLeft: "Align left",
                    justifyCenter: "Center",
                    justifyRight: "Align right",
                    justifyFull: "Justify",
                    alignTop: "Align top",
                    alignMiddle: "Align middle",
                    alignBottom: "Align bottom"
                }
            },
            mergeDialog: {
                title: "Merge cells",
                buttons: {
                    mergeCells: "Merge all",
                    mergeHorizontally: "Merge horizontally",
                    mergeVertically: "Merge vertically",
                    unmerge: "Unmerge"
                }
            },
            freezeDialog: {
                title: "Freeze panes",
                buttons: {
                    freezePanes: "Freeze panes",
                    freezeRows: "Freeze rows",
                    freezeColumns: "Freeze columns",
                    unfreeze: "Unfreeze panes"
                }
            },
            confirmationDialog: {
                text: "Are you sure you want to remove this sheet?",
                title: "Sheet remove"
            },
            validationDialog: {
                title: "Data Validation",
                hintMessage: "Please enter a valid {0} value {1}.",
                hintTitle: "Validation {0}",
                criteria: {
                    any: "Any value",
                    number: "Number",
                    text: "Text",
                    date: "Date",
                    custom: "Custom Formula",
                    list: "List"
                },
                comparers: {
                    greaterThan: "greater than",
                    lessThan: "less than",
                    between: "between",
                    notBetween: "not between",
                    equalTo: "equal to",
                    notEqualTo: "not equal to",
                    greaterThanOrEqualTo: "greater than or equal to",
                    lessThanOrEqualTo: "less than or equal to"
                },
                comparerMessages: {
                    greaterThan: "greater than {0}",
                    lessThan: "less than {0}",
                    between: "between {0} and {1}",
                    notBetween: "not between {0} and {1}",
                    equalTo: "equal to {0}",
                    notEqualTo: "not equal to {0}",
                    greaterThanOrEqualTo: "greater than or equal to {0}",
                    lessThanOrEqualTo: "less than or equal to {0}",
                    custom: "that satisfies the formula: {0}"
                },
                labels: {
                    criteria: "Criteria",
                    comparer: "Comparer",
                    min: "Min",
                    max: "Max",
                    value: "Value",
                    start: "Start",
                    end: "End",
                    onInvalidData: "On invalid data",
                    rejectInput: "Reject input",
                    showWarning: "Show warning",
                    showHint: "Show hint",
                    hintTitle: "Hint title",
                    hintMessage: "Hint message",
                    ignoreBlank: "Ignore blank"
                },
                placeholders: {
                    typeTitle: "Type title",
                    typeMessage: "Type message"
                }
            },
            exportAsDialog: {
                title: "Export...",
                labels: {
                    fileName: "File name",
                    saveAsType: "Save as type",
                    exportArea: "Export",
                    paperSize: "Paper size",
                    margins: "Margins",
                    orientation: "Orientation",
                    print: "Print",
                    guidelines: "Guidelines",
                    center: "Center",
                    horizontally: "Horizontally",
                    vertically: "Vertically"
                }
            },
            modifyMergedDialog: {
                errorMessage: "Cannot change part of a merged cell."
            },
            useKeyboardDialog: {
                title: "Copying and pasting",
                errorMessage: "These actions cannot be invoked through the menu. Please use the keyboard shortcuts instead:",
                labels: {
                    forCopy: "for copy",
                    forCut: "for cut",
                    forPaste: "for paste"
                }
            },
            unsupportedSelectionDialog: {
                errorMessage: "That action cannot be performed on multiple selection."
            }
        })), kendo.spreadsheet && kendo.spreadsheet.messages.filterMenu && (kendo.spreadsheet.messages.filterMenu = e.extend(!0, kendo.spreadsheet.messages.filterMenu, {
            sortAscending: "Sort range A to Z",
            sortDescending: "Sort range Z to A",
            filterByValue: "Filter by value",
            filterByCondition: "Filter by condition",
            apply: "Apply",
            search: "Search",
            addToCurrent: "Add to current selection",
            clear: "Clear",
            blanks: "(Blanks)",
            operatorNone: "None",
            and: "AND",
            or: "OR",
            operators: {
                string: {
                    contains: "Text contains",
                    doesnotcontain: "Text does not contain",
                    startswith: "Text starts with",
                    endswith: "Text ends with"
                },
                date: {
                    eq: "Date is",
                    neq: "Date is not",
                    lt: "Date is before",
                    gt: "Date is after"
                },
                number: {
                    eq: "Is equal to",
                    neq: "Is not equal to",
                    gte: "Is greater than or equal to",
                    gt: "Is greater than",
                    lte: "Is less than or equal to",
                    lt: "Is less than"
                }
            }
        })), kendo.spreadsheet && kendo.spreadsheet.messages.colorPicker && (kendo.spreadsheet.messages.colorPicker = e.extend(!0, kendo.spreadsheet.messages.colorPicker, {
            reset: "Reset color",
            customColor: "Custom color...",
            apply: "Apply",
            cancel: "Cancel"
        })), kendo.spreadsheet && kendo.spreadsheet.messages.toolbar && (kendo.spreadsheet.messages.toolbar = e.extend(!0, kendo.spreadsheet.messages.toolbar, {
            addColumnLeft: "Add column left",
            addColumnRight: "Add column right",
            addRowAbove: "Add row above",
            addRowBelow: "Add row below",
            alignment: "Alignment",
            alignmentButtons: {
                justtifyLeft: "Align left",
                justifyCenter: "Center",
                justifyRight: "Align right",
                justifyFull: "Justify",
                alignTop: "Align top",
                alignMiddle: "Align middle",
                alignBottom: "Align bottom"
            },
            backgroundColor: "Background",
            bold: "Bold",
            borders: "Borders",
            colorPicker: {
                reset: "Reset color",
                customColor: "Custom color..."
            },
            copy: "Copy",
            cut: "Cut",
            deleteColumn: "Delete column",
            deleteRow: "Delete row",
            excelImport: "Import from Excel...",
            filter: "Filter",
            fontFamily: "Font",
            fontSize: "Font size",
            format: "Custom format...",
            formatTypes: {
                automatic: "Automatic",
                number: "Number",
                percent: "Percent",
                financial: "Financial",
                currency: "Currency",
                date: "Date",
                time: "Time",
                dateTime: "Date time",
                duration: "Duration",
                moreFormats: "More formats..."
            },
            formatDecreaseDecimal: "Decrease decimal",
            formatIncreaseDecimal: "Increase decimal",
            freeze: "Freeze panes",
            freezeButtons: {
                freezePanes: "Freeze panes",
                freezeRows: "Freeze rows",
                freezeColumns: "Freeze columns",
                unfreeze: "Unfreeze panes"
            },
            italic: "Italic",
            merge: "Merge cells",
            mergeButtons: {
                mergeCells: "Merge all",
                mergeHorizontally: "Merge horizontally",
                mergeVertically: "Merge vertically",
                unmerge: "Unmerge"
            },
            open: "Open...",
            paste: "Paste",
            quickAccess: {
                redo: "Redo",
                undo: "Undo"
            },
            saveAs: "Save As...",
            sortAsc: "Sort ascending",
            sortDesc: "Sort descending",
            sortButtons: {
                sortSheetAsc: "Sort sheet A to Z",
                sortSheetDesc: "Sort sheet Z to A",
                sortRangeAsc: "Sort range A to Z",
                sortRangeDesc: "Sort range Z to A"
            },
            textColor: "Text Color",
            textWrap: "Wrap text",
            underline: "Underline",
            validation: "Data validation..."
        })), kendo.spreadsheet && kendo.spreadsheet.messages.view && (kendo.spreadsheet.messages.view = e.extend(!0, kendo.spreadsheet.messages.view, {
            errors: {
                shiftingNonblankCells: "Cannot insert cells due to data loss possibility. Select another insert location or delete the data from the end of your worksheet.",
                filterRangeContainingMerges: "Cannot create a filter within a range containing merges",
                validationError: "The value that you entered violates the validation rules set on the cell."
            },
            tabs: {
                home: "Home",
                insert: "Insert",
                data: "Data"
            }
        })), kendo.ui.Slider && (kendo.ui.Slider.prototype.options = e.extend(!0, kendo.ui.Slider.prototype.options, {
            increaseButtonTitle: "Increase",
            decreaseButtonTitle: "Decrease"
        })), kendo.ui.ListBox && (kendo.ui.ListBox.prototype.options.messages = e.extend(!0, kendo.ui.ListBox.prototype.options.messages, {
            tools: {
                remove: "Delete",
                moveUp: "Move Up",
                moveDown: "Move Down",
                transferTo: "Transfer To",
                transferFrom: "Transfer From",
                transferAllTo: "Transfer All To",
                transferAllFrom: "Transfer All From"
            }
        })), kendo.ui.TreeList && (kendo.ui.TreeList.prototype.options.messages = e.extend(!0, kendo.ui.TreeList.prototype.options.messages, {
            noRows: "No records to display",
            loading: "Loading...",
            requestFailed: "Request failed.",
            retry: "Retry",
            commands: {
                edit: "Edit",
                update: "Update",
                canceledit: "Cancel",
                create: "Add new record",
                createchild: "Add child record",
                destroy: "Delete",
                excel: "Export to Excel",
                pdf: "Export to PDF"
            }
        })), kendo.ui.TreeList && (kendo.ui.TreeList.prototype.options.columnMenu = e.extend(!0, kendo.ui.TreeList.prototype.options.columnMenu, {
            messages: {
                columns: "Choose columns",
                filter: "Apply filter",
                sortAscending: "Sort (asc)",
                sortDescending: "Sort (desc)"
            }
        })), kendo.ui.TreeView && (kendo.ui.TreeView.prototype.options.messages = e.extend(!0, kendo.ui.TreeView.prototype.options.messages, {
            loading: "Loading...",
            requestFailed: "Request failed.",
            retry: "Retry"
        })), kendo.ui.Upload && (kendo.ui.Upload.prototype.options.localization = e.extend(!0, kendo.ui.Upload.prototype.options.localization, {
            select: "Select files...",
            cancel: "Cancel",
            retry: "Retry",
            remove: "Remove",
            clearSelectedFiles: "Clear",
            uploadSelectedFiles: "Upload files",
            dropFilesHere: "Drop files here to upload",
            statusUploading: "uploading",
            statusUploaded: "uploaded",
            statusWarning: "warning",
            statusFailed: "failed",
            headerStatusUploading: "Uploading...",
            headerStatusUploaded: "Done",
            invalidMaxFileSize: "File size too large.",
            invalidMinFileSize: "File size too small.",
            invalidFileExtension: "File type not allowed."
        })), kendo.ui.Validator && (kendo.ui.Validator.prototype.options.messages = e.extend(!0, kendo.ui.Validator.prototype.options.messages, {
            required: "{0} is required",
            pattern: "{0} is not valid",
            min: "{0} should be greater than or equal to {1}",
            max: "{0} should be smaller than or equal to {1}",
            step: "{0} is not valid",
            email: "{0} is not valid email",
            url: "{0} is not valid URL",
            date: "{0} is not valid date",
            dateCompare: "End date should be greater than or equal to the start date"
        })), kendo.ui.progress && (kendo.ui.progress.messages = e.extend(!0, kendo.ui.progress.messages, {
            loading: "Loading..."
        })), kendo.ui.Dialog && (kendo.ui.Dialog.prototype.options.messages = e.extend(!0, kendo.ui.Dialog.prototype.options.localization, {
            close: "Close"
        })), kendo.ui.Calendar && (kendo.ui.Calendar.prototype.options.messages = e.extend(!0, kendo.ui.Calendar.prototype.options.messages, {
            weekColumnHeader: ""
        })), kendo.ui.Alert && (kendo.ui.Alert.prototype.options.messages = e.extend(!0, kendo.ui.Alert.prototype.options.localization, {
            okText: "OK"
        })), kendo.ui.Confirm && (kendo.ui.Confirm.prototype.options.messages = e.extend(!0, kendo.ui.Confirm.prototype.options.localization, {
            okText: "OK",
            cancel: "Cancel"
        })), kendo.ui.Prompt && (kendo.ui.Prompt.prototype.options.messages = e.extend(!0, kendo.ui.Prompt.prototype.options.localization, {
            okText: "OK",
            cancel: "Cancel"
        })), kendo.ui.DateInput && (kendo.ui.DateInput.prototype.options.messages = e.extend(!0, kendo.ui.DateInput.prototype.options.messages, {
            year: "year",
            month: "month",
            day: "day",
            weekday: "day of the week",
            hour: "hours",
            minute: "minutes",
            second: "seconds",
            dayperiod: "AM/PM"
        }))
    }(window.kendo.jQuery)
});
//# sourceMappingURL=kendo.messages.en-US.min.js.map
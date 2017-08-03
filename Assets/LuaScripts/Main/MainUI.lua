require("CommonFunc")

MainUI = {}
local fileName = "MainUI"

function MainUI.awake()
	CommonFunc.printLog(fileName,"awake")
end

function MainUI.start()
	CommonFunc.printLog(fileName,"start")
	
	if btnClass1 == nil then
		CommonFunc.printLog(fileName,"btnClass1 is nil")
	else
		btnClass1:GetComponent("Button").onClick:AddListener(function ()
			CommonFunc.printLog(fileName,"button Class1 Click")
			--self:GetComponent("MainUI"):LoadScene("class1/class1")
			local mainui = self.gameObject:AddComponent(typeof(CS.MainUI))
			mainui:LoadScene("class1")
		end)
		
	end
	
end

function MainUI.update()
	
end

function MainUI.ondestroy()
	CommonFunc.printLog(fileName,"destroy")
end

return MainUI

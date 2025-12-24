window.bpmnInterop = {
    modeler: null,
    
    createModeler: function (containerId) {
        if (this.modeler) {
            this.modeler.destroy();
        }

        this.modeler = new BpmnJS({
            container: '#' + containerId,
            keyboard: {
                bindTo: window
            }
        });
    },

    importXml: async function (xml) {
        try {
            await this.modeler.importXML(xml);
            var canvas = this.modeler.get('canvas');
            canvas.zoom('fit-viewport');
            return null; // Success
        } catch (err) {
            console.error('could not import BPMN 2.0 diagram', err);
            return err.message;
        }
    },

    saveXml: async function () {
        try {
            const result = await this.modeler.saveXML({ format: true });
            return result.xml;
        } catch (err) {
            console.error('could not save BPMN 2.0 diagram', err);
            throw err;
        }
    }
};
